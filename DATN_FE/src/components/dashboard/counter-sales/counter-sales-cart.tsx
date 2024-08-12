"use client";

import { useCounterSaleStore } from "@/lib/store/useCounterSaleStore";
import { useSearchAddressStore } from "@/lib/store/useSearchAddressStore";
import { useVoucherStore } from "@/lib/store/useVoucherStore";
import { validateAddress } from "@/lib/validation/validateProfile";
import { getAuthPublic } from "@/service/auth-service/auth-service";
import { Suspense, useEffect, useState } from "react";
import { toast } from "react-toastify";
import CounterSalesOrderItem from "./counter-sales-order-item";
import Loading from "@/components/shop/loading";
import { formatPrice } from "@/lib/format/format";

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
  const [isLoading, setIsLoading] = useState<boolean>(false);

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
  const createOrder = async (): Promise<FormState | undefined> => {
    const authToken = getAuthPublic();
    if (!authToken) {
      return { errors: ["Hết phiên đăng nhập, bạn cần đăng nhập lại"] };
    }

    setIsLoading(true);

    if (address === null) {
      setIsLoading(false);
      return { errors: ["Chưa có thông tin người mua"] };
    }

    //client validation
    const [errors, isValid] = validateAddress(
      address.name,
      address.email,
      address.phoneNumber,
      address.address
    );

    if (!isValid) {
      //console.log(errors);
      setIsLoading(false);
      return { errors };
    }

    try {
      const orderData: IOrderFormData = {
        name: address.name,
        email: address.email,
        phoneNumber: address.phoneNumber,
        address: address.address,
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
      //console.log(res);

      const responseData: ApiResponse<boolean> = await res.json();

      //console.log(responseData);

      const { success, message } = responseData;

      if (success) {
        // If the response is success and success is true
        sessionStorage.removeItem("orderItems");
        sessionStorage.removeItem("orderAddress");
        setIsLoading(false);
        return { success: true, errors: [] };
      } else {
        setIsLoading(false);
        return { errors: [message] };
      }
    } catch (err) {
      setIsLoading(false);
    }
  };

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    if (address === null) {
      toast.error("Chưa có thông tin người mua");
      setIsLoading(false);
      return;
    }

    try {
      const result = await createOrder();

      if (result?.success) {
        toast.success("Tạo hóa đơn thành công");
        if (typeof window !== "undefined") {
          window.location.reload(); // Reload the page to clear the form or update the UI
        }
      } else {
        // Nếu không thành công, hiện thông báo lỗi
        const errorMessage = result?.errors?.[0] || "Tạo hóa đơn thất bại";
        toast.error(errorMessage);
      }
    } catch (error) {
      toast.error("An unexpected error occurred while creating the order.");
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
                <p className="text-lg font-bold">Total:</p>
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
                >
                  {isLoading ? "Đang xử lý..." : "Tạo hóa đơn"}
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
