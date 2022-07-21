import { ProductDto } from "./ProductDto";
import { UserDto } from "./UserDto";

export interface OrderDto {
  id: number;
  userId: number;
  productId: number;
  unitvalue: number;
  amount: number;
  subtotal: number;
  iva: number;
  total: number;
  user:UserDto;
  product:ProductDto;
}
