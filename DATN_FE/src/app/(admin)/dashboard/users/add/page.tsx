import AddUserForm from "@/components/dashboard/user/add-user-form";
import { cookies as nextCookies } from "next/headers";

const User = async () => {
  //get access token form cookie
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  const res = await fetch(`http://localhost:5000/api/Account/admin/role`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
  });

  const roleSelect: ApiResponse<IRole[]> = await res.json();
  return <AddUserForm roleSelect={roleSelect.data} />;
};

const AddUserPage = () => {
  return <User />;
};
export default AddUserPage;
