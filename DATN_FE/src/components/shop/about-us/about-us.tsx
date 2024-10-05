import React from "react";
import Image from "next/image";
import Link from "next/link";

const AboutUs: React.FC = () => {
  return (
    <section className="py-24 relative">
      <div className="w-full max-w-7xl px-4 md:px-5 lg:px-5 mx-auto">
        <div className="w-full justify-start items-center gap-8 grid lg:grid-cols-2 grid-cols-1">
          <div className="w-full flex-col justify-start lg:items-start items-center gap-10 inline-flex">
            <div className="w-full flex-col justify-start lg:items-start items-center gap-4 flex">
              <h2 className="text-gray-900 text-4xl font-bold font-manrope leading-normal lg:text-start text-center">
                Chào mừng bạn đến với FStore
              </h2>
              <p className="text-gray-500 text-base font-normal leading-relaxed lg:text-start text-center">
                FStore là điểm đến hàng đầu cho các sản phẩm linh kiện điện tử
                chất lượng cao. Chúng tôi mang đến cho khách hàng những sản phẩm
                công nghệ tiên tiến, phục vụ nhu cầu từ sửa chữa, lắp ráp đến
                phát triển các dự án điện tử sáng tạo.
              </p>
            </div>
            <Link href="/product">
              <button className="sm:w-fit w-full px-3.5 py-2 bg-indigo-600 hover:bg-indigo-800 transition-all duration-700 ease-in-out rounded-lg shadow-[0px_1px_2px_0px_rgba(16,_24,_40,_0.05)] justify-center items-center flex">
                <span className="px-1.5 text-white text-sm font-medium leading-6">
                  Khám Phá Ngay
                </span>
              </button>
            </Link>
          </div>
          <div className="lg:mx-0 mx-auto h-full relative">
            <Image
              src={"/slider/tay-han-slider.jpg"}
              alt="FStore Linh Kiện Điện Tử"
              fill
              sizes="100%"
              className="object-cover rounded-3xl"
            />
          </div>
        </div>
      </div>
    </section>
  );
};

export default AboutUs;
