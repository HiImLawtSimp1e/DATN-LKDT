"use client";

import { useSearchAddressStore } from "@/lib/store/useSearchAddressStore";
import { ChangeEvent } from "react";

const CounterSalesAddressForm = () => {
  const { address, setAddress } = useSearchAddressStore();

  const handleChange = (event: ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;
    if (address) {
      setAddress({ ...address, [name]: value });
    }
  };

  return (
    <div className="flex flex-col gap-4">
      <div className="flex gap-4">
        <div className="flex-1">
          <label
            htmlFor="fullName"
            className="block mb-2 text-sm font-medium text-white"
          >
            Họ và tên
          </label>
          <input
            id="fullName"
            name="name"
            placeholder="Nhập tên khách hàng..."
            value={address?.name}
            className="text-sm rounded-lg w-full p-2.5 bg-gray-500 placeholder-gray-300 text-white"
            onChange={handleChange}
          />
        </div>
        <div className="flex-1">
          <label
            htmlFor="email"
            className="block mb-2 text-sm font-medium text-white"
          >
            Email
          </label>
          <input
            id="email"
            name="email"
            placeholder="Nhập email khách hàng..."
            value={address?.email}
            className="text-sm rounded-lg w-full p-2.5 bg-gray-500 placeholder-gray-300 text-white"
            onChange={handleChange}
          />
        </div>
        <div className="flex-1">
          <label
            htmlFor="phoneNumber"
            className="block mb-2 text-sm font-medium text-white"
          >
            Số điện thoại
          </label>
          <input
            id="phoneNumber"
            name="phoneNumber"
            placeholder="Nhập số điện thoại..."
            value={address?.phoneNumber}
            className="text-sm rounded-lg w-full p-2.5 bg-gray-500 placeholder-gray-300 text-white"
            onChange={handleChange}
          />
        </div>
      </div>
      <div className="">
        <label
          htmlFor="address"
          className="block mb-2 text-sm font-medium text-white"
        >
          Địa chỉ
        </label>
        <input
          id="address"
          name="address"
          placeholder="Enter customer address"
          value={address?.address}
          className="text-sm rounded-lg w-full p-2.5 bg-gray-500 placeholder-gray-300 text-white"
          onChange={handleChange}
        />
      </div>
    </div>
  );
};

export default CounterSalesAddressForm;
