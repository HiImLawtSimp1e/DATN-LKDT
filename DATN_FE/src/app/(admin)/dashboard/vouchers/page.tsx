import AdminLoading from "@/components/dashboard/loading";
import VoucherList from "@/components/dashboard/voucher/voucher-list";
import { cookies as nextCookies } from "next/headers";
import { Suspense } from "react";

interface IProps {
  page?: number;
  searchText?: string;
}

const Vouchers = async ({ page, searchText }: IProps) => {
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  let url = "";

  if (searchText == null || searchText == undefined) {
    url = "http://localhost:5000/api/Discount/admin";
  } else {
    url = `http://localhost:5000/api/Discount/admin/search/${searchText}`;
  }

  if (!(page == null || page <= 0)) {
    url += `?page=${page}`;
  }

  const res = await fetch(url, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`, // Thêm header Authorization
    },
    next: { tags: ["voucherListAdmin"] },
  });

  const responseData: ApiResponse<PagingParams<IAdminVoucher[]>> =
    await res.json();

  const { data, success, message } = responseData;
  // console.log(responseData);
  const { result, pages, currentPage } = data;

  return (
    <VoucherList vouchers={result} pages={pages} currentPage={currentPage} />
  );
};

const VouchersPage = ({
  searchParams,
}: {
  searchParams: { page?: number; searchText?: string };
}) => {
  // Destructure page from searchParams
  const { page, searchText } = searchParams;

  // Render Vouchers component with params prop
  return (
    <Suspense fallback={<AdminLoading />}>
      <Vouchers page={page || undefined} searchText={searchText || undefined} />
    </Suspense>
  );
};

export default VouchersPage;
