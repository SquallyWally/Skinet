import * as cuid from 'cuid';

export interface BasketItem {
  id: number;
  productName: string;
  price: number;
  quantity: number;
  pictureUrl: string;
  brand: string;
  type: string;
}

export interface Basket {
  id: string;
  basketItems: BasketItem[];
}

export class Basket implements Basket {
  id = cuid();
  basketItems: BasketItem[] = [];
}

export interface BasketTotals {
  shipping: number;
  subtotal: number;
  total: number;
}
