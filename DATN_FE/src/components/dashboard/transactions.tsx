import Image from "next/image";
import TagFiled from "../ui/tag";
import AdminNotFoundPage from "./not-found";
import { mapCssTagField, mapOrderState } from "@/lib/enums/OrderState";
import { formatDate, formatPrice } from "@/lib/format/format";
import Link from "next/link";

interface IProps {
  orders: IOrder[];
}

const Transactions = ({ orders }: IProps) => {
  return (
    <div className="my-4 bg-[#182237] p-5 rounded-lg">
      <h2 className="mb-5 text-gray-400 text-lg font-light">
        Các Đơn Hàng Mới Nhất
      </h2>

      <div className="relative overflow-x-auto">
        {orders.length == 0 && (
          <AdminNotFoundPage
            title="Not found"
            content="Không có đơn hàng nào"
          />
        )}
        {orders.length > 0 && (
          <>
            <table className="w-full text-left text-gray-400">
              <thead className="text-md text-gray-400">
                <tr>
                  <th scope="col" className="px-6 py-3">
                    Mã Đơn Hàng
                  </th>
                  <th scope="col" className="px-6 py-3">
                    Trạng Thái
                  </th>
                  <th scope="col" className="px-6 py-3">
                    Ngày Tạo
                  </th>
                  <th scope="col" className="px-6 py-3">
                    Tổng Tiền
                  </th>
                  <th scope="col" className="px-6 py-3"></th>
                </tr>
              </thead>
              <tbody>
                {orders?.map((order: IOrder, index) => (
                  <tr key={order.id}>
                    <td className="px-6 py-4 ">{order.invoiceCode}</td>
                    <td className="px-6 py-4">
                      <TagFiled
                        cssClass={mapCssTagField(order.state)}
                        context={mapOrderState(order.state)}
                      />
                    </td>
                    <td className="px-6 py-4">{formatDate(order.createdAt)}</td>
                    <td className="px-6 py-4">
                      {order.isCounterOrder
                        ? formatPrice(order.totalPrice - order.discountValue)
                        : formatPrice(
                            order.totalPrice - order.discountValue + 30000
                          )}
                    </td>
                    <td className="px-6 py-4">
                      <Link href={`/dashboard/orders/${order.id}`}>
                        <button className="m-1 px-5 py-2 bg-teal-500 text-white rounded">
                          Xem
                        </button>
                      </Link>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </>
        )}
      </div>
    </div>
  );
};

export default Transactions;
