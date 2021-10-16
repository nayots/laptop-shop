import { ProductType } from './product-type';

export interface Product {
  id: number;
  name: string;
  price: number;
  productType: ProductType;
}
