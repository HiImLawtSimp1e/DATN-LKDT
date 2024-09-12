"use client";

import { deleteAttribute } from "@/action/productAttributeAction";
import Pagination from "@/components/ui/pagination";
import Search from "@/components/ui/search";
import { useCustomActionState } from "@/lib/custom/customHook";
import { formatDate } from "@/lib/format/format";
import Link from "next/link";
import { useRouter } from "next/navigation";
import { FormEvent, useEffect, useState } from "react";
import { MdAdd } from "react-icons/md";
import { toast } from "react-toastify";
import AdminNotFoundPage from "../not-found";

interface IProps {
  productAttributes: IProductAttribute[];
  pages: number;
  currentPage: number;
}

const ProductAttributeList = ({
  productAttributes,
  pages,
  currentPage,
}: IProps) => {
  // Sử dụng cho phân trang
  const pageSize = 10;
  const startIndex = (currentPage - 1) * pageSize;

  // Sử dụng cho hành động xóa
  const router = useRouter();

  const initialState: FormState = { errors: [] };
  const [formState, formAction] = useCustomActionState<FormState>(
    deleteAttribute,
    initialState
  );

  const [toastDisplayed, setToastDisplayed] = useState(false);

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    if (
      !window.confirm(
        "Bạn có chắc chắn muốn xóa thuộc tính sản phẩm này không?"
      )
    ) {
      return;
    }
    const formData = new FormData(event.currentTarget);
    formAction(formData);
    setToastDisplayed(false); // Đặt lại toastDisplayed khi đang submit
  };

  const handleSearch = (event: FormEvent) => {
    event.preventDefault();
    const formData = new FormData(event.target as HTMLFormElement);
    const searchText = formData.get("searchText") as string;

    let url: string = `/dashboard/product-attributes`;

    if (searchText !== "" && searchText.length > 0) {
      url = `/dashboard/product-attributes?searchText=${searchText}`;
    }

    router.push(url);
  };

  useEffect(() => {
    if (formState.errors.length > 0 && !toastDisplayed) {
      toast.error(formState.errors[0]);
      setToastDisplayed(true); // Đặt toastDisplayed là true để tránh hiển thị nhiều toast
    }
    if (formState.success) {
      toast.success("Đã xóa thuộc tính sản phẩm thành công!");
      router.push("/dashboard/product-attributes");
    }
  }, [formState, toastDisplayed]);

  return (
    <div className="bg-gray-800 p-5 rounded-lg mt-5">
      <div className="flex items-center justify-between mb-5">
        <Search
          placeholder="Tìm kiếm thuộc tính..."
          name="searchText"
          onSubmit={handleSearch}
        />
        <Link href="/dashboard/product-attributes/add">
          <button className="p-2 flex items-center justify-center mb-5 bg-purple-600 text-white rounded">
            <MdAdd />
            Thêm Thuộc Tính Mới
          </button>
        </Link>
      </div>
      {productAttributes.length == 0 && (
        <AdminNotFoundPage
          title="Not found"
          content="Không tìm thấy thuộc tính sản phẩm đang tìm kiếm"
        />
      )}
      {productAttributes.length > 0 && (
        <>
          <table className="w-full text-left text-gray-400">
            <thead className="bg-gray-700 text-gray-400 uppercase">
              <tr>
                <th className="px-4 py-2">#</th>
                <th className="px-4 py-2">Tên</th>
                <th className="px-4 py-2">Ngày Tạo</th>
                <th className="px-4 py-2">Ngày Sửa</th>
                <th className="px-4 py-2">Người Tạo</th>
                <th className="px-4 py-2">Người Sửa</th>
                <th className="px-4 py-2"></th>
              </tr>
            </thead>
            <tbody>
              {productAttributes.map((attribute: IProductAttribute, index) => (
                <tr key={attribute.id} className="border-b border-gray-700">
                  <td className="px-4 py-2">{startIndex + index + 1}</td>
                  <td className="px-4 py-2">{attribute.name}</td>
                  <td className="px-4 py-2">
                    {formatDate(attribute.createdAt)}
                  </td>
                  <td className="px-4 py-2">
                    {formatDate(attribute.modifiedAt)}
                  </td>
                  <td className="px-4 py-2">{attribute.createdBy}</td>
                  <td className="px-4 py-2">{attribute.modifiedBy}</td>
                  <td className="px-4 py-2">
                    <div className="flex gap-2">
                      <Link
                        href={`/dashboard/product-attributes/${attribute.id}`}
                      >
                        <button className="m-1 px-5 py-2 bg-teal-500 text-white rounded">
                          Xem
                        </button>
                      </Link>
                      <form onSubmit={handleSubmit}>
                        <input type="hidden" name="id" value={attribute.id} />
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

export default ProductAttributeList;
