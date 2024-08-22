"use client";

import { deleteVariant } from "@/action/variantAction";
import TagField from "@/components/ui/tag";
import Link from "next/link";
import { MdAdd } from "react-icons/md";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";
import { useCustomActionState } from "@/lib/custom/customHook";
import { toast } from "react-toastify";

interface IProps {
  productId: string;
  variants: IProductVariant[];
}

const ProductVariantForm = ({ productId, variants }: IProps) => {
  const router = useRouter();

  const initialState: FormState = { errors: [] };
  const [formState, formAction] = useCustomActionState<FormState>(
    deleteVariant,
    initialState
  );

  const [toastDisplayed, setToastDisplayed] = useState(false);

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    if (!window.confirm("Bạn có chắc muốn xóa biến thể sản phẩm này không?")) {
      return;
    }
    const formData = new FormData(event.currentTarget);
    formAction(formData);
    setToastDisplayed(false); // Đặt lại toastDisplayed khi đang submit
  };

  useEffect(() => {
    if (formState.errors.length > 0 && !toastDisplayed) {
      toast.error("Xóa biến thể sản phẩm thất bại!");
      setToastDisplayed(true); // Đặt toastDisplayed là true để tránh hiển thị nhiều toast
    }
    if (formState.success) {
      toast.success("Đã xóa biến thể sản phẩm thành công!");
      router.push(`/dashboard/products/${productId}`);
    }
  }, [formState, toastDisplayed]);

  return (
    <>
      <div className="flex items-center justify-end mt-10">
        <Link
          href={{ pathname: `/dashboard/variants/add`, query: { productId } }}
        >
          <button className="p-2 flex items-center justify-center mb-5 bg-purple-600 text-white rounded">
            <MdAdd />
            Thêm Loại Sản Phẩm Mới
          </button>
        </Link>
      </div>
      <table className="w-full text-left text-gray-400">
        <thead className="bg-gray-700 text-gray-400 uppercase">
          <tr>
            <th className="px-4 py-2">#</th>
            <th className="px-4 py-2">Loại Sản Phẩm</th>
            <th className="px-4 py-2">Giá Bán</th>
            <th className="px-4 py-2">Giá Gốc</th>
            <th className="px-4 py-2">Số Lượng</th>
            <th className="px-4 py-2">Trạng Thái</th>
            <th className="px-4 py-2">Hành Động</th>
          </tr>
        </thead>
        <tbody>
          {variants?.map((variant: IProductVariant, index) => (
            <tr
              key={variant.productTypeId}
              className="border-b border-gray-700"
            >
              <td className="px-4 py-2">{index + 1}</td>
              <td className="px-4 py-2">{variant.productType.name}</td>
              <td className="px-4 py-2">{variant.price}</td>
              <td className="px-4 py-2">
                {variant.originalPrice > variant.price
                  ? variant.originalPrice
                  : variant.price}
              </td>
              <td className="px-4 py-2">{variant.quantity}</td>
              <td className="px-4 py-2">
                <TagField
                  cssClass={variant.isActive ? "bg-lime-900" : "bg-red-700"}
                  context={variant.isActive ? "Hoạt Động" : "Ngưng Hoạt Động"}
                />
              </td>
              <td className="px-4 py-2">
                <div className="flex gap-2">
                  <Link
                    href={{
                      pathname: `/dashboard/variants/${variant.productTypeId}`,
                      query: { productId },
                    }}
                  >
                    <button className="m-1 px-5 py-2 bg-blue-600 text-white rounded">
                      Sửa
                    </button>
                  </Link>

                  <form onSubmit={handleSubmit}>
                    <input type="hidden" name="productId" value={productId} />
                    <input
                      type="hidden"
                      name="productTypeId"
                      value={variant.productTypeId}
                    />
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
    </>
  );
};

export default ProductVariantForm;
