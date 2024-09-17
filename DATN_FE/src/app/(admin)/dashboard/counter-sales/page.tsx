import ProvisionOrderList from "@/components/dashboard/counter-sales/provision-order-list";
import AdminLoading from "@/components/dashboard/loading";
import { cookies as nextCookies } from "next/headers";
import { Suspense } from "react";

interface IProps {
  page?: number;
  searchText?: string;
}

const ProvisionOrders = async ({ page, searchText }: IProps) => {
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  let url = "http://localhost:5000/api/OrderCounter/provisional-order";

  if (searchText !== null && searchText !== undefined) {
    url = `http://localhost:5000/api/OrderCounter/search/provisional-order/${searchText}`;
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
    <ProvisionOrderList
      orders={result}
      pages={pages}
      currentPage={currentPage}
    />
  );
};

const ProvisionOrdersPage = ({
  searchParams,
}: {
  searchParams: { page?: number; searchText?: string };
}) => {
  // Destructure page và searchText từ searchParams
  const { page, searchText } = searchParams;

  // Render Orders component with params prop
  return (
    <Suspense fallback={<AdminLoading />}>
      <ProvisionOrders
        page={page || undefined}
        searchText={searchText || undefined}
      />
    </Suspense>
  );
};

export default ProvisionOrdersPage;
