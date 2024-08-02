import { formatDate } from "@/lib/format/format";

interface IProps {
  orderDetail: IOrderDetail;
}

const OrderDetailCustomer = ({ orderDetail }: IProps) => {
  return (
    <div className="shadow-lg rounded-lg overflow-hidden">
      <div className="px-6 py-4">
        <div className="text-2xl mb-2 font-bold text-white uppercase">
          Mã hóa đơn #{orderDetail.invoiceCode}
        </div>
        <div className="mb-2 ml-auto text-lg text-gray-400">
          Ngày đặt hàng: {formatDate(orderDetail.orderCreatedAt)}
        </div>
      </div>
      <div className="px-6 py-4 border-t border-gray-200">
        <div className="text-2xl mb-2 font-bold text-white uppercase">
          Gửi tới
        </div>
        <address className="my-6 text-lg flex flex-col gap-2 text-gray-400">
          <p>Khách hàng: {orderDetail.fullName}</p>
          <p>Email: {orderDetail.email}</p>
          <p>Địa chỉ: {orderDetail.address}</p>
          <p>Số điện thoại: {orderDetail.phone}</p>
        </address>
      </div>
    </div>
  );
};

export default OrderDetailCustomer;
