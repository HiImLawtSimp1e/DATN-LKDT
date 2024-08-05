import UserDetail from "@/components/dashboard/user/user-detail";
import { cookies as nextCookies } from "next/headers";

const User = async ({ id }: { id: string }) => {
  //get access token form cookie
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  const res = await fetch(`http://localhost:5000/api/Account/admin/${id}`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    next: { tags: ["userDetail"] },
  });

  const user: ApiResponse<IUser> = await res.json();

  return <UserDetail user={user.data} />;
};

const UserDetailPage = ({ params }: { params: { id: string } }) => {
  const { id } = params;
  return (
    <>
      <User id={id} />
    </>
  );
};
export default UserDetailPage;
