import AdminLoading from "@/components/dashboard/loading";
import OrderList from "@/components/dashboard/order/order-list";
import { cookies as nextCookies } from "next/headers";
import { Suspense } from "react";

interface IProps {
  page?: number;
  searchText?: string;
  orderState?: string;
}

const Orders = async ({ page, searchText, orderState }: IProps) => {
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  let url = "http://localhost:5000/api/Order/admin";

  if (searchText !== null && searchText !== undefined) {
    url = `http://localhost:5000/api/Order/admin/search/${searchText}`;
  }

  if (orderState !== null && orderState !== undefined) {
    url = `http://localhost:5000/api/Order/admin/filter/${orderState}`;
  }

  if (!(page == null || page <= 0)) {
    url += `?page=${page}`;
  }

  //console.log(url);

  const res = await fetch(url, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`, // Thêm header Authorization
    },
    cache: "no-store",
  });

  const responseData: ApiResponse<PagingParams<IOrder[]>> = await res.json();
  const { data, success, message } = responseData;
  //console.log(responseData);
  const { result, pages, currentPage } = data;

  return (
    <OrderList
      orders={result}
      pages={pages}
      currentPage={currentPage}
      orderState={orderState}
    />
  );
};

const OrdersPage = ({
  searchParams,
}: {
  searchParams: { page?: number; searchText?: string; orderState?: string };
}) => {
  // Destructure page, searchText và orderState từ searchParams
  const { page, searchText, orderState } = searchParams;

  // Render Orders component with params prop

  return (
    <Suspense fallback={<AdminLoading />}>
      <Orders
        page={page || undefined}
        searchText={searchText || undefined}
        orderState={orderState || undefined}
      />
    </Suspense>
  );
};

export default OrdersPage;
