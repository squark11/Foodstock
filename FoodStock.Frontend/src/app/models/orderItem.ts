export interface OrderItem {
    id?: string;
    name: string;
    expirationDate?: Date;
    orderId: string;
    quantity: number;
    categoryId: string;
    categoryName?: string;
    producentId: string;
    producentName?: string;
    barCode: string;
  }