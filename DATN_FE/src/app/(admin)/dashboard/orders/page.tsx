import OrderList from "@/components/dashboard/order/order-list";
import { cookies as nextCookies } from "next/headers";

interface IProps {
  page?: number;
  searchText?: string;
}

const Orders = async ({ page, searchText }: IProps) => {
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  let url = "";

  if (searchText == null || searchText == undefined) {
    url = "http://localhost:5000/api/Order/admin";
  } else {
    url = `http://localhost:5000/api/Order/admin/search/${searchText}`;
  }

  if (!(page == null || page <= 0)) {
    url += `?page=${page}`;
  }

  const res = await fetch(url, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`, // Thêm header Authorization
    },
    cache: "no-store",
  });

  const responseData: ApiResponse<PagingParams<IOrder[]>> = await res.json();
  const { data, success, message } = responseData;
  // console.log(responseData);
  const { result, pages, currentPage } = data;

  return <OrderList orders={result} pages={pages} currentPage={currentPage} />;
};

const OrdersPage = ({
  searchParams,
}: {
  searchParams: { page?: number; searchText?: string };
}) => {
  // Destructure page và searchText từ searchParams
  const { page, searchText } = searchParams;

  // Render Orders component with params prop
  return (
    <Orders page={page || undefined} searchText={searchText || undefined} />
  );
};

export default OrdersPage;
