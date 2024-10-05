import Link from "next/link";
import Menu from "./menu";
import NavIcons from "./nav-icons";
import SearchBar from "./search-bar";
import Image from "next/image";

const Navbar = () => {
  return (
    <div className="h-30 py-3 px-4 lg:px-8 xl:px-32 2xl:px-64 relative bg-light shadow-sm md:shadow-md">
      <div className="flex items-center justify-between md:hidden">
        {/* Moblie */}
        <Link href="/" className="">
          <Image src="/logo.png" alt="" width={60} height={10} />
        </Link>
        <Menu />
      </div>
      {/* Tablet & PC  */}
      <div className="hidden md:flex items-center justify-between gap-12 h-full">
        {/* LEFT */}
        <div className="w-1/3 xl:w-1/2 flex items-center gap-4">
          <Link href="/" className="">
            <Image src="/logo.png" alt="" width={60} height={10} />
          </Link>
          <div className="hidden xl:flex gap-4">
            <Link href="/">Trang chủ</Link>
            <Link href="/product">Sản phẩm</Link>
            <Link href="/post">Bài viết</Link>
            <Link href="/about-us">Giới thiệu</Link>
          </div>
        </div>
        {/* RIGHT */}
        <div className="mx-4 w-2/3 xl:w-1/2 flex items-center justify-between gap-8">
          <SearchBar />
          <NavIcons />
        </div>
      </div>
    </div>
  );
};

export default Navbar;
