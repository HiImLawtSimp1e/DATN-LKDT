export enum OrderState {
  Pending = 0,
  Processing = 1,
  Shipped = 2,
  Delivered = 3,
  Cancelled = 4,
}

const orderStateMapping: Record<number, string> = {
  [OrderState.Pending]: "Đang chờ",
  [OrderState.Processing]: "Đang xử lý",
  [OrderState.Shipped]: "Đang vận chuyển",
  [OrderState.Delivered]: "Thành công",
  [OrderState.Cancelled]: "Hủy bỏ",
};

const cssTagFieldMapping: Record<number, string> = {
  [OrderState.Pending]: "bg-gray-400",
  [OrderState.Processing]: "bg-yellow-900",
  [OrderState.Shipped]: "bg-green-900",
  [OrderState.Delivered]: "bg-blue-900",
  [OrderState.Cancelled]: "bg-red-900",
};

const filterMapping: Record<string, string> = {
  [OrderState.Pending]: "0",
  [OrderState.Processing]: "1",
  [OrderState.Shipped]: "2",
  [OrderState.Delivered]: "3",
  [OrderState.Cancelled]: "4",
};

export const mapOrderState = (state: number): string => {
  const result = orderStateMapping[state];
  if (!result) {
    throw new Error(`Unknown state: ${state}`);
  }
  return result;
};

export const mapFilterState = (state: string): string => {
  const result = filterMapping[state];
  if (!result) {
    throw new Error(`Unknown state: ${state}`);
  }
  return result;
};

export const mapCssTagField = (state: number): string => {
  const result = cssTagFieldMapping[state];
  if (!result) {
    throw new Error(`Unknown state: ${state}`);
  }
  return result;
};
