"use client";

import { getAuthPublic } from "@/service/auth-service/auth-service";
import React, { useEffect, useState } from "react";

interface PaymentMethodSelectProps {
  value: string;
  onChange: (value: string) => void;
}

const PaymentMethodSelect: React.FC<PaymentMethodSelectProps> = ({
  value,
  onChange,
}) => {
  const [paymentMethods, setPaymentMethods] = useState<IPaymentMethod[]>([]);
  const [loading, setLoading] = useState<boolean>(true);

  // Fetch payment methods from API
  useEffect(() => {
    const token = getAuthPublic();

    const fetchPaymentMethods = async () => {
      try {
        const res = await fetch(
          "http://localhost:5000/api/OrderCounter/select/payment-method",
          {
            method: "GET",
            headers: {
              Authorization: `Bearer ${token}`, // header Authorization
            },
          }
        );

        const responseData: ApiResponse<IPaymentMethod[]> = await res.json();
        const { data, success, message } = responseData;

        setPaymentMethods(data); // Cập nhật danh sách phương thức thanh toán
        setLoading(false); // Đặt trạng thái loading thành false khi đã load xong
      } catch (error) {
        console.error("Error fetching payment methods", error);
        setLoading(false);
      }
    };

    fetchPaymentMethods();
  }, []);

  return (
    <div>
      <label
        htmlFor="paymentMethod"
        className="block mb-2 text-sm font-medium text-white"
      >
        Phương thức thanh toán
      </label>
      <select
        id="paymentMethod"
        value={value}
        onChange={(e) => onChange(e.target.value)}
        className="text-sm rounded-lg w-full p-2.5 bg-gray-600 placeholder-gray-400 text-white"
      >
        <option value="" disabled>
          Chọn phương thức thanh toán
        </option>
        {paymentMethods.map((method) => (
          <option key={method.id} value={method.id}>
            {method.name}
          </option>
        ))}
      </select>
    </div>
  );
};

export default PaymentMethodSelect;
