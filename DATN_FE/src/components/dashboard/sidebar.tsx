"use client";

import Image from "next/image";
import {
  MdDashboard,
  MdSupervisedUserCircle,
  MdShoppingBag,
  MdWork,
  MdAnalytics,
  MdPeople,
  MdLogout,
  MdStorage,
  MdCategory,
  MdOutlinePostAdd,
  MdDvr,
  MdArticle,
  MdAirplaneTicket,
} from "react-icons/md";
import MenuLink from "./menu-link";
import { ReactNode, useEffect } from "react";
import {
  getAuthPublic,
  setLogoutPublic,
} from "@/service/auth-service/auth-service";
import { useInfoStore } from "@/lib/store/useInfoStore";

interface MenuCategory {
  title: string;
  list: MenuItem[];
}

interface MenuItem {
  title: string;
  path: string;
  icon: ReactNode;
}

const Sidebar = () => {
  const { userInfo, getUserInfo } = useInfoStore();

  const authToken = getAuthPublic();

  const handleLogout = () => {
    setLogoutPublic();
    if (typeof window !== "undefined") {
      window.location.reload();
    }
  };

  useEffect(() => {
    if (authToken) {
      getUserInfo();
    }
  }, []);

  const menuItems: MenuCategory[] = [
    {
      title: "Quản lý",
      list: [
        {
          title: "Trang chủ",
          path: "/dashboard",
          icon: <MdDashboard />,
        },
        {
          title: "Bán hàng",
          path: "/dashboard/counter-sales/add",
          icon: <MdArticle />,
        },
        {
          title: "Danh mục sản phẩm",
          path: "/dashboard/category",
          icon: <MdCategory />,
        },
        {
          title: "Sản phẩm",
          path: "/dashboard/products",
          icon: <MdShoppingBag />,
        },
        {
          title: "Loại sản phẩm",
          path: "/dashboard/product-types",
          icon: <MdStorage />,
        },
        {
          title: "Thuộc tính sản phẩm",
          path: "/dashboard/product-attributes",
          icon: <MdDvr />,
        },
        {
          title: "Đơn hàng",
          path: "/dashboard/orders",
          icon: <MdArticle />,
        },
        {
          title: "Voucher",
          path: "/dashboard/vouchers",
          icon: <MdAirplaneTicket />,
        },
        {
          title: "Tài khoản",
          path: "/dashboard/users",
          icon: <MdSupervisedUserCircle />,
        },
        {
          title: "Bài viết",
          path: "/dashboard/posts",
          icon: <MdOutlinePostAdd />,
        },
      ],
    },
  ];
  return (
    <div className="sticky top-10">
      <div className="flex items-center p-5 gap-5">
        <Image
          className="rounded-full object-cover"
          src={"/noavatar.png"}
          alt=""
          width="50"
          height="50"
        />
        <div className="flex flex-col">
          <span className="font-medium">{userInfo?.userName}</span>
          <span className="text-xs text-gray-500">{userInfo?.roleName}</span>
        </div>
      </div>
      <ul className="list-none">
        {menuItems.map((cat: MenuCategory) => (
          <li key={cat.title}>
            <span className="px-5 text-gray-500 font-bold text-xs my-2 block">
              {cat.title}
            </span>
            {cat.list.map((item: MenuItem) => (
              <MenuLink item={item} key={item.title} />
            ))}
          </li>
        ))}
      </ul>
      <span className="px-5 text-gray-500 font-bold text-xs my-2 block">
        Tài khoản
      </span>
      <div className="mx-2">
        <button
          onClick={handleLogout}
          type="submit"
          className="p-5 w-full flex items-center gap-2 my-1 rounded-lg hover:bg-gray-700"
        >
          <MdLogout />
          Đăng xuất
        </button>
      </div>
    </div>
  );
};
export default Sidebar;
