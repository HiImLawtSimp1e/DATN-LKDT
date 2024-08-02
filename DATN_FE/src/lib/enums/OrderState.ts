export enum OrderState {
  Pending = 0,
  Processing = 1,
  Shipped = 2,
  Delivered = 3,
  Cancelled = 4,
}

export const mapOrderState = (state: number): string => {
  switch (state) {
    case OrderState.Pending:
      return "Đang chờ";
    case OrderState.Processing:
      return "Đang xử lý";
    case OrderState.Shipped:
      return "Đang vận chuyển";
    case OrderState.Delivered:
      return "Thành công";
    case OrderState.Cancelled:
      return "Hủy bỏ";
    default:
      throw new Error(`Unknown state: ${state}`);
  }
};
