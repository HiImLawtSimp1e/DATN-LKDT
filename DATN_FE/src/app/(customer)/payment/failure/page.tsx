"use client";

import ErrorSvg from "@/components/ui/svg/error-svg";
import Link from "next/link";
import { useSearchParams } from "next/navigation";

const PaymentFailure = () => {
  const searchParams = useSearchParams();
  const message = searchParams.get("message");

  return (
    <div className=" mt-24 h-screen">
      <div className="bg-white p-6 md:mx-auto">
        <div className="flex items-center justify-center mb-4">
          <ErrorSvg />
        </div>
        <div className="text-center">
          <h3 className="md:text-2xl text-base text-gray-900 font-semibold text-center">
            Thanh toán thất bại!
          </h3>
          <p className="text-gray-600 my-2">
            Rất tiếc, thanh toán của bạn không thành công. Vui lòng thử lại.
          </p>
          <p>Nếu vấn đề vẫn tiếp diễn, vui lòng liên hệ với đội ngũ hỗ trợ</p>
          <p>{message ? `Lỗi: ${message}` : ""}</p>
          <div className="py-10 text-center uppercase">
            <Link
              href="/"
              className="px-12 bg-indigo-600 hover:bg-indigo-500 text-white font-semibold py-3"
            >
              Về trang chủ
            </Link>
          </div>
        </div>
      </div>
    </div>
  );
};

export default PaymentFailure;
