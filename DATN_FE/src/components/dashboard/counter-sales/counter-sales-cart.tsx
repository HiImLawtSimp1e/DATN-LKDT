import { useCounterSaleStore } from "@/lib/store/useCounterSaleStore";
import CounterSalesOrderItem from "./counter-sales-order-item";
import { formatPrice } from "@/lib/format/format";
import { useVoucherStore } from "@/lib/store/useVoucherStore";
import { useSearchAddressStore } from "@/lib/store/useSearchAddressStore";
import { Suspense, useEffect, useState } from "react";
import Loading from "@/components/shop/loading";
import { toast } from "react-toastify";
import { getAuthPublic } from "@/service/auth-service/auth-service";

interface IOrderFormData {
  name: string;
  email: string;
  phoneNumber: string;
  address: string;
  orderItems: IOrderItem[];
}

const CounterSaleCart = () => {
  const { orderItems } = useCounterSaleStore();
  const { address } = useSearchAddressStore();
  const { voucher } = useVoucherStore();
  const [isLoading, setIsLoading] = useState(false);

  const totalAmount = orderItems.reduce(
    (accumulator, currentValue) =>
      accumulator + currentValue.price * currentValue.quantity,
    0
  );

  let discount: number = 0;
  if (voucher && totalAmount > voucher.minOrderCondition) {
    discount = voucher.isDiscountPercent
      ? Math.min(
          totalAmount * (voucher.discountValue / 100),
          voucher.maxDiscountValue
        )
      : voucher.discountValue;
  }

  // Hàm tạo đơn hàng
  const createOrder = async () => {
    const authToken = getAuthPublic();
    if (!authToken) {
      console.error("No auth token available.");
      return;
    }

    setIsLoading(true);

    if (address === null) {
      setIsLoading(false);
      return;
    }

    try {
      const orderData: IOrderFormData = {
        name: address.name,
        email: address.email,
        phoneNumber: address.phoneNumber,
        address: address.phoneNumber,
        orderItems: orderItems,
      };

      const res = await fetch(
        `http://localhost:5000/api/OrderCounter/create-order`,
        {
          method: "POST",
          headers: {
            Authorization: `Bearer ${authToken}`,
            "Content-Type": "application/json",
          },
          body: JSON.stringify(orderData),
        }
      );

      console.log(res);

      const responseData: ApiResponse<boolean> = await res.json();

      console.log(responseData);

      const { data } = responseData;

      if (data === false) {
        setIsLoading(false);
      }

      sessionStorage.removeItem("orderItems");
      sessionStorage.removeItem("orderAddress");
      setIsLoading(false);

      if (typeof window !== "undefined") {
        window.location.reload();
      }
    } catch (err) {
      setIsLoading(false);
    }
  };

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    if (address === null) {
      toast.error("Địa chỉ khách hàng không được bỏ trống");
    } else {
      createOrder();
      toast.success("Tạo mới đơn hàng thành công");
    }
  };

  const [isClient, setIsClient] = useState(false);

  useEffect(() => {
    setIsClient(true);
  }, []);

  return (
    <div className="h-[150vh]">
      <div className="flex flex-col gap-4">
        {orderItems.length > 0 ? (
          <>
            <div className="p-5 flex flex-col gap-1 rounded-lg bg-gray-600">
              {isClient ? (
                orderItems.map((item: IOrderItem, index: number) => (
                  <Suspense key={index} fallback={<Loading />}>
                    <CounterSalesOrderItem item={item} />
                  </Suspense>
                ))
              ) : (
                <Loading />
              )}
            </div>

            <div className="h-full p-6 shadow-md text-gray-100 rounded-lg bg-gray-600">
              <div className="mb-2 flex justify-between">
                <p>Tạm tính:</p>
                <p>{formatPrice(totalAmount)}</p>
              </div>
              {discount > 0 && (
                <div className="flex justify-between">
                  <p>Giảm giá:</p>
                  <p>{formatPrice(discount)}</p>
                </div>
              )}
              <hr className="my-8" />
              <div className="flex justify-between">
                <p className="text-lg font-bold">Tổng tiền:</p>
                <div className="">
                  <p className="mb-1 text-2xl font-bold">
                    {formatPrice(totalAmount - discount)}
                  </p>
                  <p className="text-sm text-gray-300">đã bao gồm VAT</p>
                </div>
              </div>
              <form onSubmit={handleSubmit}>
                <button
                  type="submit"
                  className="mt-6 w-full rounded-md bg-blue-500 text-2xl py-4 font-medium text-blue-50 md:text-lg md:py-2 hover:bg-blue-600"
                  disabled={isLoading}
                >
                  {isLoading ? "Đang xử lý..." : "Tạo đơn hàng"}
                </button>
              </form>
            </div>
          </>
        ) : (
          <h1 className="mb-10 text-center text-gray-600 text-2xl font-bold opacity-80">
            Đơn hàng trống
          </h1>
        )}
      </div>
    </div>
  );
};

export default CounterSaleCart;
