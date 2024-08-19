interface IOrder {
  id: string;
  invoiceCode: string;
  totalPrice: number;
  originalPrice: number;
  discountValue: number;
  state: number;
  createdAt: string;
  modifiedAt: string;
  createdBy: string;
  modifiedBy: string;
}

interface IOrderItem {
  productId: string;
  productTypeId: string;
  productTitle: string;
  productTypeName: string;
  price: number;
  originalPrice: number;
  imageUrl?: string;
  quantity: number;
}

interface IOrderDetail {
  id: string;
  fullName: string;
  email: string;
  phone: string;
  address: string;
  invoiceCode: string;
  discountValue: number;
  state: number;
  orderCreatedAt: string;
}
