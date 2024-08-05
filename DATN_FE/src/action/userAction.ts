"use server";

import {
  validateAddUser,
  validateUpdateUser,
} from "@/lib/validation/validateUser";
import { revalidatePath, revalidateTag } from "next/cache";
import { cookies as nextCookies } from "next/headers";

interface AddUserFormData {
  username: string;
  password: string;
  name: string;
  email: string;
  phoneNumber: string;
  address: string;
  roleId: string;
}

interface UpdateUserFormData {
  name: string;
  email: string;
  phoneNumber: string;
  address: string;
  isActive?: boolean | null;
}

export const createUser = async (
  prevState: FormState,
  formData: FormData
): Promise<FormState | undefined> => {
  const username = formData.get("username") as string;
  const password = formData.get("password") as string;
  const name = formData.get("name") as string;
  const email = formData.get("email") as string;
  const phoneNumber = formData.get("phoneNumber") as string;
  const address = formData.get("address") as string;
  const roleId = formData.get("roleId") as string;

  const [errors, isValid] = validateAddUser(
    username,
    password,
    name,
    email,
    phoneNumber,
    address
  );

  if (!isValid) {
    return { errors };
  }

  const userData: AddUserFormData = {
    username,
    password,
    name,
    email,
    phoneNumber,
    address,
    roleId,
  };

  //get access token form cookie
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  try {
    const res = await fetch("http://localhost:5000/api/Account/admin", {
      method: "POST",
      body: JSON.stringify(userData),
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    });

    if (!res.ok) {
      // Xử lý lỗi từ server
      const errorResponse = await res.json();
      console.error(`Lỗi server: ${JSON.stringify(errorResponse)}`);

      // Kiểm tra xem phản hồi lỗi có chứa trường message không
      if (errorResponse && errorResponse.message) {
        return { errors: [errorResponse.message] };
      } else {
        return { errors: ["Đã xảy ra lỗi server."] };
      }
    }

    // Phân tích dữ liệu phản hồi nếu thành công
    const responseData: ApiResponse<string> = await res.json();
    const { success, message } = responseData;

    if (success) {
      // Nếu phản hồi thành công, cập nhật lại đường dẫn và chuyển hướng
      revalidatePath("/dashboard/users");
      return { success: true, errors: [] };
    } else {
      return { errors: [message] };
    }
  } catch (error) {
    console.error(`Lỗi phân tích JSON: ${error}`);
    return { errors: ["Lỗi phản hồi từ server."] };
  }
};

export const updateUser = async (
  prevState: FormState,
  formData: FormData
): Promise<FormState | undefined> => {
  const id = formData.get("id") as string;
  const name = formData.get("name") as string;
  const email = formData.get("email") as string;
  const phoneNumber = formData.get("phoneNumber") as string;
  const address = formData.get("address") as string;
  const isActive = formData.get("isActive") === "true";

  const [errors, isValid] = validateUpdateUser(
    name,
    email,
    phoneNumber,
    address
  );

  if (!isValid) {
    return { errors };
  }

  const userData: UpdateUserFormData = {
    name,
    email,
    phoneNumber,
    address,
    isActive,
  };

  //get access token form cookie
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  try {
    const res = await fetch(`http://localhost:5000/api/Account/admin/${id}`, {
      method: "PUT",
      body: JSON.stringify(userData),
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    });

    if (!res.ok) {
      // Xử lý lỗi từ server
      const errorResponse = await res.json();
      console.error(`Lỗi server: ${JSON.stringify(errorResponse)}`);

      // Kiểm tra xem phản hồi lỗi có chứa trường message không
      if (errorResponse && errorResponse.message) {
        return { errors: [errorResponse.message] };
      } else {
        return { errors: ["Đã xảy ra lỗi server."] };
      }
    }

    // Phân tích dữ liệu phản hồi nếu thành công
    const responseData: ApiResponse<string> = await res.json();
    const { success, message } = responseData;

    if (success) {
      // Nếu phản hồi thành công, cập nhật lại đường dẫn và chuyển hướng
      revalidatePath("/dashboard/users");
      revalidateTag("userDetail");
      return { success: true, errors: [] };
    } else {
      return { errors: [message] };
    }
  } catch (error) {
    console.error(`Lỗi phân tích JSON: ${error}`);
    return { errors: ["Lỗi phản hồi từ server."] };
  }
};

export const deleteUser = async (
  prevState: FormState,
  formData: FormData
): Promise<FormState | undefined> => {
  const id = formData.get("id") as string;

  //get access token form cookie
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  const res = await fetch(`http://localhost:5000/api/Account/admin/${id}`, {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
  });

  const responseData: ApiResponse<string> = await res.json();
  // console.log(responseData);
  const { success, message } = responseData;

  if (success) {
    // Nếu phản hồi thành công, cập nhật lại đường dẫn và chuyển hướng
    revalidatePath("/dashboard/users");
    return { success: true, errors: [] };
  } else {
    return { errors: [message] };
  }
};
