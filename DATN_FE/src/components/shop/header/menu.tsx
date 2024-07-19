"use client";

import { useState } from "react";
import Image from "next/image";
import Link from "next/link";

const Menu = () => {
  const [open, setOpen] = useState<boolean>(false);

  return (
    <div>
      <Image
        src="/menu.png"
        alt=""
        width={28}
        height={28}
        className="cursor-pointer"
        onClick={() => setOpen((prev) => !prev)}
      />
      {open && (
        <div className="absolute bg-white text-zinc-800 left-0 top-20 w-full z-10">
          <div className="flex mb-4 flex-col items-center justify-center gap-8 text-xl z-10">
            <Link href="/">Trang chủ</Link>
            <Link href="/shop">Sản phẩm</Link>
            <Link href="/post">Tin tức</Link>
            <Link href="/">Giới thiệu</Link>
            <Link href="/">Đăng xuất</Link>
            <Link href="/">Giỏ hàng (1)</Link>
          </div>
        </div>
      )}
    </div>
  );
};

export default Menu;
