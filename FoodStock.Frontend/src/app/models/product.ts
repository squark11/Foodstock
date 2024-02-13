import { Category } from "./category";
import { Producent } from "./producent";
import { Supplier } from "./supplier";
import { User } from "./user";

export interface Product {
    id: string;
    name: string;
    categoryId: string;
    categoryName: string;
    producentId: string;
    producentName:string;
    expirationDate: string;
    quantity: number;
    barCode: string;
    addedDate?: string;
    user:User;
    userId: string;
    userFirstName:string;
    userSurname:string;
    deliveryDate: string;
    supplierId: string;
    supplierName: string;
}