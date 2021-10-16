import { ProductType } from './product-type';

export interface CartItem {
  productID: number;
  productName: string;
  price: number;
  productType: ProductType;
}
