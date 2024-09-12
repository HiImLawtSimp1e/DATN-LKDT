"use client";

import { deleteVoucher } from "@/action/voucherAction";
import Pagination from "@/components/ui/pagination";
import Search from "@/components/ui/search";
import TagFiled from "@/components/ui/tag";
import { useCustomActionState } from "@/lib/custom/customHook";
import { formatDate, formatPrice } from "@/lib/format/format";
import Link from "next/link";
import { useRouter } from "next/navigation";
import { FormEvent, useEffect, useState } from "react";
import { MdAdd } from "react-icons/md";
import { toast } from "react-toastify";
import AdminNotFoundPage from "../not-found";

interface IProps {
  vouchers: IAdminVoucher[];
  pages: number;
  currentPage: number;
}

const VoucherList = ({ vouchers, pages, currentPage }: IProps) => {
  //using for pagination
  const pageSize = 10;
  const startIndex = (currentPage - 1) * pageSize;

  //using for delete action
  const router = useRouter();

  const initialState: FormState = { errors: [] };
  const [formState, formAction] = useCustomActionState<FormState>(
    deleteVoucher,
    initialState
  );

  const [toastDisplayed, setToastDisplayed] = useState(false);

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    if (!window.confirm("Bạn có muốn xóa voucher này không?")) {
      return;
    }
    const formData = new FormData(event.currentTarget);
    formAction(formData);
    setToastDisplayed(false); // Reset toastDisplayed when submitting
  };

  const handleSearch = (event: FormEvent) => {
    event.preventDefault();
    const formData = new FormData(event.target as HTMLFormElement);
    const searchText = formData.get("searchText") as string;

    let url: string = `/dashboard/vouchers`;

    if (searchText !== "" && searchText.length > 0) {
      url = `/dashboard/vouchers?searchText=${searchText}`;
    }

    router.push(url);
  };

  useEffect(() => {
    if (formState.errors.length > 0 && !toastDisplayed) {
      toast.error(formState.errors[0]);
      setToastDisplayed(true); // Set toastDisplayed to true to prevent multiple toasts
    }
    if (formState.success) {
      toast.success("Xóa voucher thành công");
      router.push("/dashboard/vouchers");
    }
  }, [formState, toastDisplayed]);

  return (
    <div className="bg-gray-800 p-5 rounded-lg mt-5">
      <div className="flex items-center justify-between mb-5">
        <Search
          placeholder="Tìm kiếm voucher..."
          name="searchText"
          onSubmit={handleSearch}
        />
        <Link href="/dashboard/vouchers/add">
          <button className="p-2 flex items-center justify-center mb-5 bg-purple-600 text-white rounded">
            <MdAdd />
            Thêm mới Voucher
          </button>
        </Link>
      </div>
      {vouchers.length == 0 && (
        <AdminNotFoundPage
          title="Not found"
          content="Không tìm thấy voucher đang tìm kiếm"
        />
      )}
      {vouchers.length > 0 && (
        <>
          <table className="w-full text-left text-gray-400 overflow-x-auto">
            <thead className="bg-gray-700 text-gray-400 uppercase">
              <tr>
                <th className="px-4 py-2">#</th>
                <th className="px-4 py-2">Mã giảm giá</th>

                <th className="px-4 py-2">Loại giảm giá</th>
                <th className="px-4 py-2">Giảm giá</th>
                <th className="px-4 py-2">Trạng thái</th>
                <th className="px-4 py-2">Ngày tạo</th>
                <th className="px-4 py-2">Ngày sửa</th>
                <th className="px-4 py-2">Người tạo</th>
                <th className="px-4 py-2">Người sửa</th>
                <th className="px-4 py-2"></th>
              </tr>
            </thead>
            <tbody>
              {vouchers.map((voucher: IAdminVoucher, index) => (
                <tr key={voucher.id} className="border-b border-gray-700">
                  <td className="px-4 py-2">{startIndex + index + 1}</td>
                  <td className="px-4 py-2">{voucher.code}</td>
                  <td className="px-4 py-2">
                    <TagFiled
                      cssClass={
                        voucher.isDiscountPercent
                          ? "bg-green-700"
                          : "bg-blue-900"
                      }
                      context={
                        voucher.isDiscountPercent ? "Phần trăm" : "Cố định"
                      }
                    />
                  </td>
                  <td className="px-4 py-2">
                    {voucher.isDiscountPercent
                      ? `${voucher.discountValue}%`
                      : formatPrice(voucher.discountValue)}
                  </td>
                  <td className="px-4 py-2">
                    <TagFiled
                      cssClass={voucher.isActive ? "bg-lime-900" : "bg-red-700"}
                      context={
                        voucher.isActive ? "Hoạt Động" : "Ngưng Hoạt Động"
                      }
                    />
                  </td>
                  <td className="px-4 py-2">{formatDate(voucher.createdAt)}</td>
                  <td className="px-4 py-2">
                    {formatDate(voucher.modifiedAt)}
                  </td>
                  <td className="px-4 py-2">{voucher.createdBy}</td>
                  <td className="px-4 py-2">{voucher.modifiedBy}</td>
                  <td className="px-4 py-2">
                    <div className="flex gap-2">
                      <Link href={`/dashboard/vouchers/${voucher.id}`}>
                        <button className="m-1 px-5 py-2 bg-teal-500 text-white rounded">
                          Xem
                        </button>
                      </Link>
                      <form onSubmit={handleSubmit}>
                        <input type="hidden" name="id" value={voucher.id} />
                        <button className="m-1 px-5 py-2 bg-red-500 text-white rounded">
                          Xóa
                        </button>
                      </form>
                    </div>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
          <Pagination
            pages={pages}
            currentPage={currentPage}
            pageSize={pageSize}
            clsColor="gray"
          />
        </>
      )}
    </div>
  );
};

export default VoucherList;
