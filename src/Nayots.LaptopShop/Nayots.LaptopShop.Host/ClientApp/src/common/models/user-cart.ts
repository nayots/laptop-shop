import { CartItem } from './cart-item';

export interface UserCart {
  cartItems: CartItem[];
  totalPrice: number;
}
