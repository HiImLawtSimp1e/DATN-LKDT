"use client";

import { deleteProduct } from "@/action/productAction";
import Pagination from "@/components/ui/pagination";
import Search from "@/components/ui/search";
import TagField from "@/components/ui/tag";
import { useCustomActionState } from "@/lib/custom/customHook";
import {
  formatDate,
  formatPrice,
  formatProductType,
} from "@/lib/format/format";
import Link from "next/link";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";
import { MdAdd } from "react-icons/md";
import { toast } from "react-toastify";

interface IProps {
  products: IProduct[];
  pages: number;
  currentPage: number;
}

const ProductList = ({ products, pages, currentPage }: IProps) => {
  // Sử dụng cho phân trang
  const pageSize = 10;
  const startIndex = (currentPage - 1) * pageSize;

  // Sử dụng cho hành động xóa
  const router = useRouter();

  const initialState: FormState = { errors: [] };
  const [formState, formAction] = useCustomActionState<FormState>(
    deleteProduct,
    initialState
  );

  const [toastDisplayed, setToastDisplayed] = useState(false);

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    if (!window.confirm("Bạn có chắc muốn xóa sản phẩm này không?")) {
      return;
    }
    const formData = new FormData(event.currentTarget);
    formAction(formData);
    setToastDisplayed(false); // Đặt lại toastDisplayed khi đang submit
  };

  useEffect(() => {
    if (formState.errors.length > 0 && !toastDisplayed) {
      toast.error("Xóa sản phẩm thất bại!");
      setToastDisplayed(true); // Đặt toastDisplayed là true để tránh hiển thị nhiều toast
    }
    if (formState.success) {
      toast.success("Đã xóa sản phẩm thành công!");
      router.push("/dashboard/products");
    }
  }, [formState, toastDisplayed]);

  return (
    <div className="bg-gray-800 p-5 rounded-lg mt-5">
      <div className="flex items-center justify-between mb-5">
        <Search placeholder="Tìm kiếm sản phẩm..." />
        <Link href="/dashboard/products/add">
          <button className="p-2 flex items-center justify-center mb-5 bg-purple-600 text-white rounded">
            <MdAdd />
            Thêm Sản Phẩm Mới
          </button>
        </Link>
      </div>
      <table className="w-full text-left text-gray-400">
        <thead className="bg-gray-700 text-gray-400 uppercase">
          <tr>
            <th className="px-4 py-2">#</th>
            <th className="px-4 py-2">Tiêu Đề</th>
            <th className="px-4 py-2">Loại sản phẩm</th>
            <th className="px-4 py-2">Giá</th>
            <th className="px-4 py-2">Trạng Thái</th>
            <th className="px-4 py-2">Ngày Tạo</th>
            <th className="px-4 py-2">Ngày Sửa</th>
            <th className="px-4 py-2">Người Tạo</th>
            <th className="px-4 py-2">Người Sửa</th>
            <th className="px-4 py-2"></th>
          </tr>
        </thead>
        <tbody>
          {products.map((product: IProduct, index) => (
            <tr key={product.id} className="border-b border-gray-700">
              <td className="px-4 py-2">{startIndex + index + 1}</td>
              <td className="px-4 py-2">{product.title}</td>
              <td className="px-4 py-2">
                {product.productVariants.map(
                  (variant: IProductVariant, index) => (
                    <div key={index}>
                      {formatProductType(variant.productType.name)}
                    </div>
                  )
                )}
              </td>
              <td className="px-4 py-2">
                {product.productVariants.map(
                  (variant: IProductVariant, index) => (
                    <div key={index}>{formatPrice(variant.price)}</div>
                  )
                )}
              </td>
              <td className="px-4 py-2">
                <TagField
                  cssClass={product.isActive ? "bg-lime-900" : "bg-red-700"}
                  context={product.isActive ? "Hoạt Động" : "Ngưng Hoạt Động"}
                />
              </td>
              <td className="px-4 py-2">{formatDate(product.createdAt)}</td>
              <td className="px-4 py-2">{formatDate(product.modifiedAt)}</td>
              <td className="px-4 py-2">{product.createdBy}</td>
              <td className="px-4 py-2">{product.modifiedBy}</td>
              <td className="px-4 py-2">
                <div className="flex gap-2">
                  <Link href={`/dashboard/products/${product.id}`}>
                    <button className="m-1 px-5 py-2 bg-teal-500 text-white rounded">
                      Xem
                    </button>
                  </Link>
                  <form onSubmit={handleSubmit}>
                    <input type="hidden" name="id" value={product.id} />
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
    </div>
  );
};
export default ProductList;
