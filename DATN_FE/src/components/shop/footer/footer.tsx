import Link from "next/link";
import Image from "next/image";

const Footer = () => {
  return (
    <div className="py-24 px-4 md:px-8 lg:px-16 xl:32 2xl:px-64 bg-light text-sm">
      {/* TOP */}
      <div className="flex flex-col md:flex-row justify-between gap-24">
        {/* LEFT */}
        <div className="w-full md:w-1/2 lg:w-1/4 flex flex-col gap-8">
          <Link href="/">
            <div className="text-2xl tracking-wide uppercase">
              Linh kiện điện tử FStore
            </div>
          </Link>
          <p>104, Trung Liệt, Đống Đa, Hà Nội</p>
          <span className="font-semibold">hungnguyenquang@fpt.edu</span>
          <span className="font-semibold">0243 558 9666</span>
          <div className="flex gap-6">
            <Image src="/facebook.png" alt="" width={16} height={16} />
            <Image src="/instagram.png" alt="" width={16} height={16} />
            <Image src="/youtube.png" alt="" width={16} height={16} />
            <Image src="/pinterest.png" alt="" width={16} height={16} />
            <Image src="/x.png" alt="" width={16} height={16} />
          </div>
        </div>
        {/* CENTER */}
        <div className="hidden lg:flex gap-16 w-1/2">
          <div className="flex flex-col justify-between">
            <h1 className="font-medium text-lg">VỀ CÔNG TY CHÚNG TÔI</h1>
            <div className="flex flex-col gap-6">
              <Link href="">Trang chủ</Link>
              <Link href="/product">Sản phẩm</Link>
              <Link href="/post">Tin tức</Link>
              <Link href="">Giới thiệu</Link>
              <Link href="">Liên hệ</Link>
            </div>
          </div>
          <div className="flex flex-col justify-between">
            <h1 className="font-medium text-lg">SẢN PHẨM</h1>
            <div className="flex flex-col gap-6">
              <Link href="/">Sản phẩm mới</Link>
              <Link href="/product?category=arduino">Arduino</Link>
              <Link href="/product?category=tay-han">Tay hàn</Link>
              <Link href="/product?category=cam-bien">Cảm biến</Link>
              <Link href="/product">Tất cả sản phẩm</Link>
            </div>
          </div>
        </div>
        {/* RIGHT */}
        <div className="w-full md:w-1/2 lg:w-1/4 flex flex-col gap-8">
          <h1 className="font-medium text-lg">ĐĂNG KÝ</h1>
          <p>
            Hãy là người đầu tiên nhận tin tức mới nhất về xu hướng, khuyến mãi
            và nhiều hơn nữa!
          </p>
          <div className="flex">
            <input type="text" placeholder="Email" className="p-4 w-3/4" />
            <button className="w-1/4 bg-red-500 text-white">Gửi</button>
          </div>
          <div className="flex justify-between">
            <Image src="/discover.png" alt="" width={40} height={20} />
            <Image src="/skrill.png" alt="" width={40} height={20} />
            <Image src="/paypal.png" alt="" width={40} height={20} />
            <Image src="/mastercard.png" alt="" width={40} height={20} />
            <Image src="/visa.png" alt="" width={40} height={20} />
          </div>
        </div>
      </div>
      {/* BOTTOM */}
      <div className="flex flex-col md:flex-row items-center justify-between gap-8 mt-16">
        <div className="">
          © 2024 by Fpoly SD-18 Summer. All Rights Reserved.
        </div>
        <div className="flex flex-col gap-8 md:flex-row">
          <div className="">
            <span className="text-gray-500 mr-4">Ngôn ngữ</span>
            <span className="font-medium">Việt Nam | Tiếng Việt</span>
          </div>
          <div className="">
            <span className="text-gray-500 mr-4">Tiền tệ</span>
            <span className="font-medium">VND</span>
          </div>
        </div>
      </div>
    </div>
  );
};
export default Footer;
