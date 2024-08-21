"use server";

import { revalidatePath, revalidateTag } from "next/cache";
import { cookies as nextCookies } from "next/headers";

// Định nghĩa hàm thêm loại sản phẩm
export const addType = async (
  prevState: FormState,
  formData: FormData
): Promise<FormState | undefined> => {
  // Trích xuất các trường cần thiết từ formData
  const name = formData.get("name") as string;

  //get access token form cookie
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  try {
    const res = await fetch(`http://localhost:5000/api/ProductType/admin`, {
      method: "POST",
      body: JSON.stringify({ name }),
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    });

    if (!res.ok) {
      // Xử lý phản hồi lỗi từ server
      const errorResponse = await res.json();
      const { errors } = errorResponse;

      // Tạo một mảng để chứa các thông báo lỗi
      let errorMessages: string[] = [];

      // Kiểm tra xem có lỗi cụ thể nào và thêm chúng vào mảng thông báo lỗi
      if (errors) {
        for (const key in errors) {
          if (errors.hasOwnProperty(key)) {
            errorMessages = errorMessages.concat(errors[key]);
          }
        }
      }

      // Trả về trạng thái đã cập nhật với thông báo lỗi
      return { errors: errorMessages };
    }

    // Nếu phản hồi thành công, phân tích dữ liệu phản hồi
    const responseData: ApiResponse<string> = await res.json();
    const { success, message } = responseData;

    if (success) {
      // Nếu phản hồi thành công và success là true, cập nhật lại đường dẫn và chuyển hướng
      revalidateTag("selectProductType");
      revalidateTag("productTypeList");
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

// Định nghĩa hàm cập nhật loại sản phẩm
export const updateType = async (
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
      `http://localhost:5000/api/ProductType/admin/${id}`,
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
      // Xử lý phản hồi lỗi từ server
      const errorResponse = await res.json();
      const { errors } = errorResponse;

      // Tạo một mảng để chứa các thông báo lỗi
      let errorMessages: string[] = [];

      // Kiểm tra xem có lỗi cụ thể nào và thêm chúng vào mảng thông báo lỗi
      if (errors) {
        for (const key in errors) {
          if (errors.hasOwnProperty(key)) {
            errorMessages = errorMessages.concat(errors[key]);
          }
        }
      }

      // Trả về trạng thái đã cập nhật với thông báo lỗi
      return { errors: errorMessages };
    }

    // Nếu phản hồi thành công, phân tích dữ liệu phản hồi
    const responseData: ApiResponse<string> = await res.json();
    // console.log(responseData);
    const { success, message } = responseData;

    if (success) {
      // Nếu phản hồi thành công, cập nhật lại đường dẫn và chuyển hướng
      revalidateTag("selectProductType");
      revalidateTag("productTypeList");
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

// Định nghĩa hàm xóa loại sản phẩm
export const deleteType = async (
  prevState: FormState,
  formData: FormData
): Promise<FormState | undefined> => {
  const id = formData.get("id") as string;

  //get access token form cookie
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  const res = await fetch(`http://localhost:5000/api/ProductType/admin/${id}`, {
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
    revalidateTag("selectProductType");
    revalidateTag("productTypeList");
    revalidatePath("/");
    revalidatePath("/product");
    return { success: true, errors: [] };
  } else {
    return { errors: [message] };
  }
};
