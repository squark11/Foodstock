import { OrderItem } from "./orderItem";
import { Organization } from "./organization";
import { Supplier } from "./supplier";

export interface Order {
    id?: string;
    orderName: string;
    orderDate: Date;
    acceptanceOfTheOrderDate: Date;
    orderBy: string;
    acceptedBy: string;
    orderStatus: string;
    organization?: Organization; 
    supplier: Supplier;
    orderItems: OrderItem[];
  }