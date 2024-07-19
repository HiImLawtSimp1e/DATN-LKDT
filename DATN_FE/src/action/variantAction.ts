"use server";

// Import các module và interface cần thiết
import { validateVariant } from "@/lib/validation/validateVariant";
import { revalidateTag } from "next/cache";

// Định nghĩa interface VariantFormData
interface VariantFormData {
  productTypeId: string;
  price: number | null;
  originalPrice: number | null;
  quantity?: number | null;
  isActive?: boolean;
}

// Định nghĩa hàm addVariant
export const addVariant = async (
  prevState: FormState,
  formData: FormData
): Promise<FormState | undefined> => {
  // Trích xuất các trường cần thiết từ formData
  const productId = formData.get("productId") as string;
  const productTypeId = formData.get("productTypeId") as string;
  const price = formData.get("price") ? Number(formData.get("price")) : null;
  const originalPrice = formData.get("originalPrice")
    ? Number(formData.get("originalPrice"))
    : null;

  // Validate các trường trích xuất
  const [errors, isValid] = validateVariant(
    productTypeId,
    price,
    originalPrice
  );

  // Nếu dữ liệu không hợp lệ, trả về các lỗi
  if (!isValid) {
    return { errors };
  }

  // Chuẩn bị dữ liệu biến thể
  const variantData: VariantFormData = {
    productTypeId,
    price,
    originalPrice,
  };

  try {
    const res = await fetch(
      `http://localhost:5000/api/ProductVariant/admin/${productId}`,
      {
        method: "POST",
        body: JSON.stringify(variantData),
        headers: { "Content-Type": "application/json" },
      }
    );

    if (!res.ok) {
      // Nếu phản hồi không OK, phân tích phản hồi lỗi
      const errorResponse = await res.json();
      const { errors } = errorResponse;

      // Tạo mảng để lưu thông báo lỗi
      let errorMessages: string[] = [];

      // Kiểm tra xem có lỗi cụ thể nào và thêm chúng vào mảng thông báo lỗi
      if (errors) {
        for (const key in errors) {
          if (errors.hasOwnProperty(key)) {
            errorMessages = errorMessages.concat(errors[key]);
          }
        }
      }
      // Trả về trạng thái cập nhật với các lỗi
      return { errors: errorMessages };
    }

    const responseData: ApiResponse<string> = await res.json();
    // console.log(responseData);
    const { success, message } = responseData;

    if (success) {
      // Nếu phản hồi thành công, cập nhật lại đường dẫn và chuyển hướng
      revalidateTag("productListAdmin");
      revalidateTag("productDetailAdmin");
      revalidateTag("selectProductType");
      revalidateTag("shopProductDetail");
      return { success: true, errors: [] };
    } else {
      return { errors: [message] };
    }
  } catch (error) {
    // Xử lý các lỗi không mong muốn
    console.error("Lỗi không mong đợi:", error);
    return { errors: ["Đã xảy ra lỗi không mong đợi. Vui lòng thử lại."] };
  }
};

// Định nghĩa hàm updateVariant
export const updateVariant = async (
  prevState: FormState,
  formData: FormData
): Promise<FormState | undefined> => {
  // Trích xuất các trường cần thiết từ formData
  const productId = formData.get("productId") as string;
  const productTypeId = formData.get("productTypeId") as string;
  const price = formData.get("price") ? Number(formData.get("price")) : null;
  const originalPrice = formData.get("originalPrice")
    ? Number(formData.get("originalPrice"))
    : null;
  const quantity = formData.get("quantity")
    ? Number(formData.get("quantity"))
    : null;
  const isActive = formData.get("isActive") === "true";

  // Validate các trường trích xuất
  const [errors, isValid] = validateVariant(
    productTypeId,
    price,
    originalPrice,
    quantity
  );

  // Nếu dữ liệu không hợp lệ, trả về các lỗi
  if (!isValid) {
    return { errors };
  }

  // Chuẩn bị dữ liệu biến thể
  const variantData: VariantFormData = {
    productTypeId,
    price,
    originalPrice,
    quantity,
    isActive,
  };

  try {
    const res = await fetch(
      `http://localhost:5000/api/ProductVariant/admin/${productId}`,
      {
        method: "PUT",
        body: JSON.stringify(variantData),
        headers: { "Content-Type": "application/json" },
      }
    );

    if (!res.ok) {
      // Nếu phản hồi không OK, phân tích phản hồi lỗi
      const errorResponse = await res.json();
      const { errors } = errorResponse;

      // Tạo mảng để lưu thông báo lỗi
      let errorMessages: string[] = [];

      // Kiểm tra xem có lỗi cụ thể nào và thêm chúng vào mảng thông báo lỗi
      if (errors) {
        for (const key in errors) {
          if (errors.hasOwnProperty(key)) {
            errorMessages = errorMessages.concat(errors[key]);
          }
        }
      }
      // Trả về trạng thái cập nhật với các lỗi
      return { errors: errorMessages };
    }

    // Nếu phản hồi OK, phân tích dữ liệu phản hồi
    const responseData: ApiResponse<string> = await res.json();
    // console.log(responseData);
    const { success, message } = responseData;

    if (success) {
      // Nếu phản hồi thành công, cập nhật lại đường dẫn và chuyển hướng
      revalidateTag("getVariant");
      revalidateTag("productListAdmin");
      revalidateTag("productDetailAdmin");
      revalidateTag("selectProductType");
      revalidateTag("productDetailAdmin");
      revalidateTag("shopProductDetail");
      return { success: true, errors: [] };
    } else {
      return { errors: [message] };
    }
  } catch (error) {
    // Xử lý các lỗi không mong muốn
    console.error("Lỗi không mong đợi:", error);
    return { errors: ["Đã xảy ra lỗi không mong đợi. Vui lòng thử lại."] };
  }
};

// Định nghĩa hàm deleteVariant
export const deleteVariant = async (
  prevState: FormState,
  formData: FormData
): Promise<FormState | undefined> => {
  const productId = formData.get("productId") as string;
  const productTypeId = formData.get("productTypeId") as string;

  const res = await fetch(
    `http://localhost:5000/api/ProductVariant/admin/${productId}?productTypeId=${productTypeId}`,
    {
      method: "DELETE",
      headers: { "Content-Type": "application/json" },
    }
  );

  const responseData: ApiResponse<string> = await res.json();
  // console.log(responseData);
  const { success, message } = responseData;

  if (success) {
    // Nếu phản hồi thành công, cập nhật lại đường dẫn và chuyển hướng
    revalidateTag("getVariant");
    revalidateTag("productListAdmin");
    revalidateTag("productDetailAdmin");
    revalidateTag("selectProductType");
    revalidateTag("shopProductDetail");
    return { success: true, errors: [] };
  } else {
    return { errors: [message] };
  }
};
