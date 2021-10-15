import { ProductType } from './product-type';

export interface CartItem {
  productId: number;
  productName: string;
  price: number;
  productType: ProductType;
}
