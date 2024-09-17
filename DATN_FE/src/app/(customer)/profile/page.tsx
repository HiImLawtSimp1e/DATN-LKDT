import Loading from "@/components/shop/loading";
import UserProfile from "@/components/shop/profile/user-profile";
import { cookies as nextCookies } from "next/headers";
import { Suspense } from "react";

const Profile = async () => {
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  const res = await fetch("http://localhost:5000/api/Address", {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`, // Thêm header Authorization
    },
    next: { tags: ["addressList"] },
  });

  const responseData: ApiResponse<PagingParams<IAddress[]>> = await res.json();
  const { data, success, message } = responseData;
  // console.log(responseData);
  const { result, pages, currentPage, pageResults } = data;
  return (
    <UserProfile
      addresses={result}
      pages={pages}
      currentPage={currentPage}
      pageSize={pageResults}
    />
  );
};

const ProfilePage = () => {
  return (
    <Suspense fallback={<Loading />}>
      <Profile />
    </Suspense>
  );
};

export default ProfilePage;
