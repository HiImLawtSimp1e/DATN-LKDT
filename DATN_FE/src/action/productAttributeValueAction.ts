"use server";

import { revalidatePath, revalidateTag } from "next/cache";
import { cookies as nextCookies } from "next/headers";

// Định nghĩa interface ProductAttributeValueFormData
interface ProductAttributeValueFormData {
  productAttributeId: string;
  value: string;
  isActive?: boolean;
}

// Định nghĩa hàm addAttributeValue
export const addAttributeValue = async (
  prevState: FormState,
  formData: FormData
): Promise<FormState | undefined> => {
  // Trích xuất các trường cần thiết từ formData
  const productId = formData.get("productId") as string;
  const productAttributeId = formData.get("productAttributeId") as string;
  const value = formData.get("value") as string;

  // Chuẩn bị dữ liệu giá trị thuộc tính sản phẩm
  const attributeValueData: ProductAttributeValueFormData = {
    productAttributeId,
    value,
  };

  //get access token form cookie
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  try {
    const res = await fetch(
      `http://localhost:5000/api/ProductValue/admin/${productId}`,
      {
        method: "POST",
        body: JSON.stringify(attributeValueData),
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
      }
    );

    if (!res.ok) {
      // Nếu không phản hồi thành công, phân tích phản hồi lỗi
      const errorResponse = await res.json();
      const { errors } = errorResponse;

      // Tạo mảng để lưu trữ các thông báo lỗi
      let errorMessages: string[] = [];

      // Kiểm tra và thêm các thông báo lỗi cụ thể vào mảng
      if (errors) {
        for (const key in errors) {
          if (errors.hasOwnProperty(key)) {
            errorMessages = errorMessages.concat(errors[key]);
          }
        }
      }
      // Trả về trạng thái cập nhật với các thông báo lỗi
      return { errors: errorMessages };
    }

    // Nếu phản hồi thành công, phân tích dữ liệu phản hồi
    const responseData: ApiResponse<string> = await res.json();
    // console.log(responseData);
    const { success, message } = responseData;

    if (success) {
      // Nếu phản hồi thành công, cập nhật lại đường dẫn và chuyển hướng
      revalidateTag("productListAdmin");
      revalidateTag("productDetailAdmin");
      revalidateTag("selectProductAttribute");
      revalidateTag("shopProductDetail");
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

// Định nghĩa hàm updateAttributeValue
export const updateAttributeValue = async (
  prevState: FormState,
  formData: FormData
): Promise<FormState | undefined> => {
  // Trích xuất các trường cần thiết từ formData
  const productId = formData.get("productId") as string;
  const productAttributeId = formData.get("productAttributeId") as string;
  const value = formData.get("value") as string;
  const isActive = formData.get("isActive") === "true";

  // Chuẩn bị dữ liệu giá trị thuộc tính sản phẩm
  const attributeValueData: ProductAttributeValueFormData = {
    productAttributeId,
    value,
    isActive,
  };

  //get access token form cookie
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  try {
    const res = await fetch(
      `http://localhost:5000/api/ProductValue/admin/${productId}`,
      {
        method: "PUT",
        body: JSON.stringify(attributeValueData),
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
      }
    );

    if (!res.ok) {
      // Nếu không phản hồi thành công, phân tích phản hồi lỗi
      const errorResponse = await res.json();
      const { errors } = errorResponse;

      // Tạo mảng để lưu trữ các thông báo lỗi
      let errorMessages: string[] = [];
      // Kiểm tra và thêm các thông báo lỗi cụ thể vào mảng
      if (errors) {
        for (const key in errors) {
          if (errors.hasOwnProperty(key)) {
            errorMessages = errorMessages.concat(errors[key]);
          }
        }
      }

      // Trả về trạng thái cập nhật với các thông báo lỗi
      return { errors: errorMessages };
    }

    // Nếu phản hồi thành công, phân tích dữ liệu phản hồi
    const responseData: ApiResponse<string> = await res.json();
    // console.log(responseData);
    const { success, message } = responseData;

    if (success) {
      // Nếu phản hồi thành công, cập nhật lại đường dẫn và chuyển hướng
      revalidateTag("getVariant");
      revalidateTag("productListAdmin");
      revalidateTag("productDetailAdmin");
      revalidateTag("selectProductAttribute");
      revalidateTag("productDetailAdmin");
      revalidateTag("shopProductDetail");
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

// Định nghĩa hàm deleteAttributeValue
export const deleteAttributeValue = async (
  prevState: FormState,
  formData: FormData
) => {
  // Trích xuất các trường cần thiết từ formData
  const productId = formData.get("productId") as string;
  const productAttributeId = formData.get("productAttributeId") as string;

  //get access token form cookie
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  const res = await fetch(
    `http://localhost:5000/api/ProductValue/admin/${productId}?productAttributeId=${productAttributeId}`,
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
    // Nếu phản hồi thành công, cập nhật lại đường dẫn và chuyển hướng
    revalidateTag("getAttributeValue");
    revalidateTag("productListAdmin");
    revalidateTag("productDetailAdmin");
    revalidateTag("selectProductAttribute");
    revalidateTag("shopProductDetail");
    revalidatePath("/");
    revalidatePath("/product");
    return { success: true, errors: [] };
  } else {
    return { errors: [message] };
  }
};
