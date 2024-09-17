import Loading from "@/components/shop/loading";
import PaymentCart from "@/components/shop/payment/payment-cart";
import { cookies as nextCookies } from "next/headers";
import { Suspense } from "react";

const Payment = async () => {
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  const res = await fetch("http://localhost:5000/api/Cart", {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`, // Thêm header Authorization
    },
    next: { tags: ["shoppingCart"] },
  });

  const cartItems: ApiResponse<ICartItem[]> = await res.json();
  // console.log(response.data);

  const addressRes = await fetch("http://localhost:5000/api/Address/main", {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`, // Thêm header Authorization
    },
    next: { tags: ["mainAddress"] },
  });

  const address: ApiResponse<IAddress> = await addressRes.json();
  // console.log(response.data);

  return <PaymentCart cartItems={cartItems.data} address={address.data} />;
};

const ShoppingCartPage = () => {
  return (
    <Suspense fallback={<Loading />}>
      <Payment />
    </Suspense>
  );
};

export default ShoppingCartPage;
