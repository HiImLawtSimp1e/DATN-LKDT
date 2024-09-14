"use client";

import { applyVoucher } from "@/action/orderCounterAction";
import { useCustomActionState } from "@/lib/custom/customHook";
import { formatPrice } from "@/lib/format/format";
import { useCounterSaleStore } from "@/lib/store/useCounterSaleStore";
import { useVoucherStore } from "@/lib/store/useVoucherStore";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";

const CounterSaleVoucher = () => {
  const generateVoucherInfo = (voucher: IVoucher): string => {
    if (totalAmount < voucher.minOrderCondition) {
      return `Voucher "${
        voucher.code
      }" chỉ áp dụng cho đơn hàng có giá trị từ ${formatPrice(
        voucher.minOrderCondition
      )} trở lên`;
    }
    let result = voucher.isDiscountPercent
      ? `Đang áp dụng voucher "${voucher.code}", giảm ${voucher.discountValue}%`
      : `Đang áp dụng voucher "${voucher.code}", giảm ${formatPrice(
          voucher.discountValue
        )}`;

    if (voucher.isDiscountPercent && voucher.maxDiscountValue > 0) {
      result += `, tối đa ${formatPrice(voucher.maxDiscountValue)}`;
    }

    if (voucher.minOrderCondition > 0) {
      result += `, cho đơn hàng từ ${formatPrice(voucher.minOrderCondition)}`;
    }

    return result;
  };

  //for state manager
  const { voucher, setVoucher } = useVoucherStore();
  const { totalAmount } = useCounterSaleStore();

  const voucherInfo = voucher ? generateVoucherInfo(voucher) : "";

  //for action
  const initialState: FormStateData<IVoucher> = { errors: [] };
  const [formState, formAction] = useCustomActionState<FormStateData<IVoucher>>(
    applyVoucher,
    initialState
  );

  const [toastDisplayed, setToastDisplayed] = useState(false);

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const formData = new FormData(event.currentTarget);
    if (totalAmount !== null && totalAmount > 0) {
      formData.set("totalAmount", totalAmount.toString());
    }
    formAction(formData);
    setToastDisplayed(false); // Reset toastDisplayed when submitting
  };

  useEffect(() => {
    if (formState.errors.length > 0 && !toastDisplayed) {
      toast.error(formState.errors[0]);
      setToastDisplayed(true); // Set toastDisplayed to true to prevent multiple toasts
    }
    if (formState.success) {
      toast.success("Áp dụng voucher thành công!");
      if (formState.data !== undefined) {
        const voucher: IVoucher = formState.data;
        setVoucher(voucher);
      }
    }
  }, [formState, toastDisplayed]);
  return (
    <>
      <div className="h-full mt-6 shadow-md">
        <form onSubmit={handleSubmit}>
          <div className="flex gap-1">
            <input
              type="text"
              placeholder="Nhập mã voucher..."
              name="discountCode"
              className="text-black p-4 w-3/4"
            />
            <button
              type="submit"
              className="px-4 w-2/5 bg-red-500 text-white uppercase"
            >
              Áp dụng
            </button>
          </div>
        </form>
      </div>
      {voucher && voucherInfo && totalAmount > 0 && (
        <div
          className={`py-1 px-3 text-lg leading-2 ${
            totalAmount > voucher.minOrderCondition
              ? "text-green-400"
              : "text-red-600"
          } `}
        >
          {voucherInfo}
        </div>
      )}
    </>
  );
};

export default CounterSaleVoucher;
