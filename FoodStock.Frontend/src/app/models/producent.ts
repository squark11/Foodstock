import { Product } from "./product";

export interface Producent {
  id?: string;
  name: string;
  products?: Product[];
}