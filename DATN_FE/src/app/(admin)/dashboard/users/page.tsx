import UserList from "@/components/dashboard/user/user-list";
import { cookies as nextCookies } from "next/headers";

interface IProps {
  page?: number;
  searchText?: string;
}

const Users = async ({ page, searchText }: IProps) => {
  //get access token form cookie
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  let url = "";

  if (searchText == null || searchText == undefined) {
    url = "http://localhost:5000/api/Account/admin";
  } else {
    url = `http://localhost:5000/api/Account/admin/search/${searchText}`;
  }

  if (!(page == null || page <= 0)) {
    url += `?page=${page}`;
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
  searchParams: { page?: number; searchText?: string };
}) => {
  // Destructure page from searchParams
  const { page, searchText } = searchParams;

  // Render Users component with params prop
  return (
    <Users page={page || undefined} searchText={searchText || undefined} />
  );
};

export default UsersPage;
