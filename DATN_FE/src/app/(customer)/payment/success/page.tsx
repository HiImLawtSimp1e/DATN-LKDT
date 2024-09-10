"use client";

import SuccessSvg from "@/components/ui/svg/success-svg";
import Link from "next/link";
import { useSearchParams } from "next/navigation";

const PaymentSuccess = () => {
  const searchParams = useSearchParams();
  const orderId = searchParams.get("orderId");

  return (
    <div className="mt-24 h-screen">
      <div className="bg-white p-6 md:mx-auto">
        <div className="flex items-center justify-center mb-4">
          <SuccessSvg />
        </div>
        <div className="text-center">
          <h3 className="md:text-2xl text-base text-gray-900 font-semibold text-center">
            Thanh toán đơn hàng thành công!
          </h3>
          <p className="text-gray-600 my-2">Cảm ơn quý khách</p>
          <p>Chúc quý khách một ngày tốt lành!</p>
          <div className="py-10 text-center">
            <Link
              href="/order-history"
              className="px-12 bg-indigo-600 hover:bg-indigo-500 text-white font-semibold py-3"
            >
              Tới đơn hàng của tôi
            </Link>
          </div>
        </div>
      </div>
    </div>
  );
};

export default PaymentSuccess;
