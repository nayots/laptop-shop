import { ProductType } from './product-type';

export interface Product {
  iD: number;
  name: string;
  price: number;
  productType: ProductType;
}
