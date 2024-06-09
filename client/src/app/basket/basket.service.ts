import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Basket, IBasket, IBasketItem, IBasketTotal } from '../shared/models/basket';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  baseUrl ='http://localhost:9010'

  constructor(private http:HttpClient) { }

  private basketSource = new BehaviorSubject<IBasket|null>(null)
  basketSource$ = this.basketSource.asObservable();

  private basketTotal = new BehaviorSubject<IBasketTotal|null>(null)
  basketTotal$ = this.basketTotal.asObservable();

  getBasket(userName:string){
    return this.http.get<IBasket>(
      `${this.baseUrl}/Basket/GetBasket/santiago`
    ).subscribe({
      next:basket=>{
        this.basketSource.next(basket);
        this.calculateBasketTotal()
      }
    });
  }

  setBasket(basket:IBasket){
    return this.http.post<IBasket>(
      `${this.baseUrl}/Basket/CreateBasket`,basket
    ).subscribe({
      next:basket=>{
        this.basketSource.next(basket);
        this.calculateBasketTotal()
      }
    })
  }

  getCurrentBasket(){
    return this.basketSource.value;
  }

  addItemToBasket(item:IProduct,quatity=1){
    const itemToAdd:IBasketItem=this.mapProductItemToBasketItem(item);
    const basket=this.getCurrentBasket()??this.createBasket();
    basket.items=this.addOrUpdateItem(basket.items,itemToAdd,quatity);
    this.setBasket(basket);
  }

  incrementItemQuantity(item:IBasketItem){
    const basket= this.getCurrentBasket();
    if(!basket) return;
    const foundIndex=basket.items.findIndex((x)=>x.productId===item.productId)
    basket.items[foundIndex].quantity++;
    this.setBasket(basket);
  }

  removeItemFromBasket(item:IBasketItem){
    const basket= this.getCurrentBasket();
    if(!basket) return;
    if(basket.items.some((x)=>x.productId === item.productId)){
      basket.items= basket.items.filter(x=>x.productId!==item.productId)
      if(basket.items.length>0){
        this.setBasket(basket)
      }else{
        this.deleteBasket(basket.userName);
      }
    }
  }

  deleteBasket(userName: string) {
    this.http.delete(
      `${this.baseUrl}/Basket/DeleteBasket/${userName}`
    ).subscribe({
      next:(response)=>{
        this.basketSource.next(null);
        this.basketTotal.next(null);
        localStorage.removeItem('basket_user');
      },
      error:(err)=>{
        console.error('Error delenting basket')
        console.error(err)
      }
    })
  }

  decrementItemQuantity(item:IBasketItem){
    const basket= this.getCurrentBasket();
    if(!basket) return;
    const foundIndex=basket.items.findIndex((x)=>x.productId===item.productId)
    if(basket.items[foundIndex].quantity>1){
      basket.items[foundIndex].quantity--;
      this.setBasket(basket);
    }else{
      this.removeItemFromBasket(item);
    }
  }

  private createBasket(): Basket {
    const basket = new Basket();
    localStorage.setItem('basket_user','santiago');
    return basket;
  }

  private mapProductItemToBasketItem(item: IProduct): IBasketItem {
    return{
      productId:item.id,
      productName:item.name,
      price:item.price,
      imageFile:item.imageFile,
      quantity:0
    }
  }

  private addOrUpdateItem(items: IBasketItem[], itemToAdd: IBasketItem, quatity: number): IBasketItem[] {
    const item= items.find(x=>x.productId==itemToAdd.productId);
    if(item){
      item.quantity+=quatity;
    }else{
      itemToAdd.quantity=quatity;
      items.push(itemToAdd);
    }
    return items;
  }

  private calculateBasketTotal(){
    const basket = this.getCurrentBasket()
    if(!basket) return;

    const total = basket.items.reduce((x,y)=>(y.price * y.quantity)+x,0)
    this.basketTotal.next({total});
  }

}


