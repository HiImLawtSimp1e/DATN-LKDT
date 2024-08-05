import UserList from "@/components/dashboard/user/user-list";
import { cookies as nextCookies } from "next/headers";

const Users = async ({ params }: { params: { page?: number } }) => {
  //get access token form cookie
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  const { page } = params;
  let url = "";

  if (page == null || page <= 0) {
    url = `http://localhost:5000/api/Account/admin`;
  } else {
    url = `http://localhost:5000/api/Account/admin?page=${page}`;
  }

  const res = await fetch(url, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
  });

  const responseData: ApiResponse<PagingParams<IUser[]>> = await res.json();
  const { data, success, message } = responseData;
  // console.log(responseData);
  const { result, pages, currentPage } = data;

  return <UserList users={result} pages={pages} currentPage={currentPage} />;
};

const UsersPage = async ({
  searchParams,
}: {
  searchParams: { page?: number };
}) => {
  // Destructure page from searchParams
  const { page } = searchParams;

  // Render Users component with params prop
  return <Users params={{ page: page || undefined }} />;
};

export default UsersPage;
