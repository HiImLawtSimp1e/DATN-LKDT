"use client";

import Pagination from "@/components/ui/pagination";
import { formatDate, formatPrice } from "@/lib/format/format";
import Link from "next/link";
import TagFiled from "@/components/ui/tag";
import {
  mapCssTagField,
  mapFilterState,
  mapOrderState,
  OrderState,
} from "@/lib/enums/OrderState";
import Search from "@/components/ui/search";
import { MdRefresh } from "react-icons/md";
import { FormEvent } from "react";
import { useRouter } from "next/navigation";
import AdminNotFoundPage from "../not-found";

interface IProps {
  orders: IOrder[];
  pages: number;
  currentPage: number;
  orderState?: string;
}

const OrderList = ({ orders, pages, currentPage, orderState }: IProps) => {
  const pageSize = 10;
  const startIndex = (currentPage - 1) * pageSize;

  const router = useRouter();

  const handleReload = () => {
    if (window !== undefined) {
      window.location.reload();
    }
  };

  const handleSearch = (event: FormEvent) => {
    event.preventDefault();
    const formData = new FormData(event.target as HTMLFormElement);
    const searchText = formData.get("searchText") as string;

    let url: string = `/dashboard/orders`;

    if (searchText !== "" && searchText.length > 0) {
      url = `/dashboard/orders?searchText=${searchText}`;
    }

    router.push(url);
  };

  const handleChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const selectedValue = e.target.value;
    if (selectedValue === "") {
      // Khi chọn "Tất cả", đẩy đến /dashboard/orders mà không có orderState
      router.push("/dashboard/orders");
    } else {
      // Khi chọn trạng thái khác, đẩy đến đường dẫn có query orderState
      router.push(
        `/dashboard/orders?orderState=${mapFilterState(selectedValue)}`
      );
    }
  };

  return (
    <div className="bg-gray-800 p-5 rounded-lg mt-5">
      <div className="flex items-center justify-between mb-5">
        <div className="flex items-center justify-between">
          <Search
            placeholder="Tìm kiếm đơn hàng..."
            name="searchText"
            onSubmit={handleSearch}
          />
          <div className="mx-4 mb-8 px-3">
            <label
              htmlFor="state"
              className="block mb-2 text-sm font-medium text-white"
            >
              Trạng thái hóa đơn
            </label>
            <select
              className="text-sm rounded-lg w-full p-2.5 bg-gray-600 placeholder-gray-400 text-white"
              id="state"
              value={orderState || ""}
              onChange={handleChange}
            >
              <option key="" value="">
                Tất cả
              </option>
              {Object.keys(OrderState)
                .filter((key) => isNaN(Number(key)))
                .map((key) => {
                  const stateValue = OrderState[key as keyof typeof OrderState];
                  return (
                    <option key={stateValue} value={stateValue}>
                      {mapOrderState(stateValue)}
                    </option>
                  );
                })}
            </select>
          </div>
        </div>
        <button
          onClick={() => handleReload()}
          className="p-2 px-4 flex items-center justify-center mb-4.5 bg-blue-600 text-white rounded"
        >
          <MdRefresh />
          Tải lại
        </button>
      </div>
      {orders.length == 0 && (
        <AdminNotFoundPage
          title="Not found"
          content="Không tìm thấy đơn hàng đang tìm kiếm"
        />
      )}
      {orders.length > 0 && (
        <>
          <table className="w-full text-left text-gray-400">
            <thead className="bg-gray-700 text-gray-400 uppercase">
              <tr>
                <th className="px-4 py-2">#</th>
                <th className="px-4 py-2">Mã đơn hàng</th>
                <th className="px-4 py-2">Trạng thái</th>
                <th className="px-4 py-2">Ngày tạo</th>
                <th className="px-4 py-2">Ngày sửa</th>
                <th className="px-4 py-2">Người tạo</th>
                <th className="px-4 py-2">Người sửa</th>
                <th className="px-4 py-2">Tổng tiền</th>
                <th className="px-4 py-2"></th>
              </tr>
            </thead>
            <tbody>
              {orders?.map((order: IOrder, index) => (
                <tr key={order.id} className="border-b border-gray-700">
                  <td className="px-4 py-2">{startIndex + index + 1}</td>
                  <td className="px-4 py-2">{order.invoiceCode}</td>
                  <td className="px-4 py-2">
                    <TagFiled
                      cssClass={mapCssTagField(order.state)}
                      context={mapOrderState(order.state)}
                    />
                  </td>
                  <td className="px-4 py-2">{formatDate(order.createdAt)}</td>
                  <td className="px-4 py-2">{formatDate(order.modifiedAt)}</td>
                  <td className="px-4 py-2">{order.createdBy}</td>
                  <td className="px-4 py-2">{order.modifiedBy}</td>
                  <td className="px-4 py-2">
                    {order.isCounterOrder
                      ? formatPrice(order.totalPrice - order.discountValue)
                      : formatPrice(
                          order.totalPrice - order.discountValue + 30000
                        )}
                  </td>
                  <td className="px-4 py-2">
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
          <Pagination
            pages={pages}
            currentPage={currentPage}
            pageSize={pageSize}
            clsColor="gray"
          />
        </>
      )}
    </div>
  );
};

export default OrderList;
