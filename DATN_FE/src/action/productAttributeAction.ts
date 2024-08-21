"use server";

import { revalidatePath, revalidateTag } from "next/cache";
import { cookies as nextCookies } from "next/headers";

// Định nghĩa hàm addAttribute
export const addAttribute = async (
  prevState: FormState,
  formData: FormData
): Promise<FormState | undefined> => {
  // Trích xuất các trường cần thiết từ formData
  const name = formData.get("name") as string;

  //get access token form cookie
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  try {
    const res = await fetch(
      `http://localhost:5000/api/ProductAttribute/admin`,
      {
        method: "POST",
        body: JSON.stringify({ name }),
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
      }
    );

    if (!res.ok) {
      // Nếu phản hồi không thành công, phân tích phản hồi lỗi
      const errorResponse = await res.json();
      const { errors } = errorResponse;

      // Tạo mảng để chứa thông báo lỗi
      let errorMessages: string[] = [];

      // Kiểm tra và thêm thông báo lỗi vào mảng
      if (errors) {
        for (const key in errors) {
          if (errors.hasOwnProperty(key)) {
            errorMessages = errorMessages.concat(errors[key]);
          }
        }
      }

      // Trả về trạng thái mới với các lỗi
      return { errors: errorMessages };
    }

    // Nếu phản hồi thành công, phân tích dữ liệu phản hồi
    const responseData: ApiResponse<string> = await res.json();
    const { success, message } = responseData;

    if (success) {
      // Nếu thành công, làm mới lại thẻ và chuyển hướng
      revalidateTag("selectProductAttribute");
      revalidateTag("productAttributeList");
      revalidatePath("/");
      revalidatePath("/product");
      return { success: true, errors: [] };
    } else {
      return { errors: [message] };
    }
  } catch (error) {
    // Xử lý các lỗi không mong muốn
    console.error("Lỗi không mong muốn:", error);
    return { errors: ["Đã xảy ra lỗi không mong muốn. Vui lòng thử lại."] };
  }
};

// Định nghĩa hàm updateAttribute
export const updateAttribute = async (
  prevState: FormState,
  formData: FormData
): Promise<FormState | undefined> => {
  // Trích xuất các trường cần thiết từ formData
  const id = formData.get("id") as string;
  const name = formData.get("name") as string;

  //get access token form cookie
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  try {
    const res = await fetch(
      `http://localhost:5000/api/ProductAttribute/admin/${id}`,
      {
        method: "PUT",
        body: JSON.stringify({ id, name }),
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
      }
    );

    if (!res.ok) {
      // Nếu phản hồi không thành công, phân tích phản hồi lỗi
      const errorResponse = await res.json();
      const { errors } = errorResponse;

      // Tạo mảng để chứa thông báo lỗi
      let errorMessages: string[] = [];

      // Kiểm tra và thêm thông báo lỗi vào mảng
      if (errors) {
        for (const key in errors) {
          if (errors.hasOwnProperty(key)) {
            errorMessages = errorMessages.concat(errors[key]);
          }
        }
      }

      // Trả về trạng thái mới với các lỗi
      return { errors: errorMessages };
    }

    // Nếu phản hồi thành công, phân tích dữ liệu phản hồi
    const responseData: ApiResponse<string> = await res.json();
    // console.log(responseData);
    const { success, message } = responseData;

    if (success) {
      // Nếu thành công, làm mới lại thẻ và chuyển hướng
      revalidateTag("selectProductAttribute");
      revalidateTag("productAttributeList");
      revalidatePath("/");
      revalidatePath("/product");
      return { success: true, errors: [] };
    } else {
      return { errors: [message] };
    }
  } catch (error) {
    // Xử lý các lỗi không mong muốn
    console.error("Lỗi không mong muốn:", error);
    return { errors: ["Đã xảy ra lỗi không mong muốn. Vui lòng thử lại."] };
  }
};

// Định nghĩa hàm deleteAttribute
export const deleteAttribute = async (
  prevState: FormState,
  formData: FormData
): Promise<FormState | undefined> => {
  const id = formData.get("id") as string;

  //get access token form cookie
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  const res = await fetch(
    `http://localhost:5000/api/ProductAttribute/admin/${id}`,
    {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    }
  );

  const responseData: ApiResponse<string> = await res.json();
  // console.log(responseData);
  const { success, message } = responseData;

  if (success) {
    // Nếu phản hồi thành công, làm mới lại thẻ và chuyển hướng
    revalidateTag("selectProductAttribute");
    revalidateTag("productAttributeList");
    revalidatePath("/");
    revalidatePath("/product");
    return { success: true, errors: [] };
  } else {
    return { errors: [message] };
  }
};
