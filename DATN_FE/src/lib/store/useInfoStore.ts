import { getAuthPublic } from "@/service/auth-service/auth-service";
import { create } from "zustand";

type InfoState = {
  userInfo: IUserInfo | null;
  isLoading: boolean;
  getUserInfo: () => void;
};

export const useInfoStore = create<InfoState>((set) => ({
  userInfo: null,
  isLoading: false,
  getUserInfo: async () => {
    const authToken = getAuthPublic();
    if (!authToken) {
      // Nếu không có token, không gọi API và có thể thông báo cho người dùng hoặc xử lý theo cách khác
      console.error("No auth token available.");
      return;
    }

    set({ isLoading: true }); // Bắt đầu trạng thái loading

    try {
      const res = await fetch(
        "http://localhost:5000/api/Auth/get-user-claims",
        {
          method: "GET",
          headers: {
            Authorization: `Bearer ${authToken}`,
          },
          cache: "no-store",
        }
      );

      if (!res.ok) {
        // Kiểm tra xem phản hồi có phải là lỗi hay không
        throw new Error("Network response was not ok.");
      }

      const responseData: ApiResponse<IUserInfo> = await res.json();
      const { data, success, message } = responseData;

      if (!success) {
        // Xử lý trường hợp không thành công (ví dụ: thông báo lỗi)
        console.error(message);
        return;
      }

      set({
        userInfo: data,
        isLoading: false,
      });
    } catch (err) {
      console.error("Failed to fetch cart:", err);
      // Xử lý lỗi, ví dụ: thông báo lỗi cho người dùng hoặc ghi log
    } finally {
      set({ isLoading: false }); // Kết thúc trạng thái loading
    }
  },
}));
