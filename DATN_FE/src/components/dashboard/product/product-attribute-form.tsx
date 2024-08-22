"use client";

import { deleteAttributeValue } from "@/action/productAttributeValueAction";
import TagField from "@/components/ui/tag";
import { useCustomActionState } from "@/lib/custom/customHook";
import Link from "next/link";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";
import { MdAdd } from "react-icons/md";
import { toast } from "react-toastify";

interface IProps {
  productId: string;
  productValues: IProductValue[];
}

const ProductAttributeValueForm = ({ productId, productValues }: IProps) => {
  const router = useRouter();

  const initialState: FormState = { errors: [] };
  const [formState, formAction] = useCustomActionState<FormState>(
    deleteAttributeValue,
    initialState
  );

  const [toastDisplayed, setToastDisplayed] = useState(false);

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    if (
      !window.confirm(
        "Bạn có chắc muốn xóa giá trị thuộc tính sản phẩm này không?"
      )
    ) {
      return;
    }
    const formData = new FormData(event.currentTarget);
    formAction(formData);
    setToastDisplayed(false); // Reset toastDisplayed when submitting
  };

  useEffect(() => {
    if (formState.errors.length > 0 && !toastDisplayed) {
      toast.error("Xóa giá trị thuộc tính sản phẩm thất bại!");
      setToastDisplayed(true); // Set toastDisplayed to true to prevent multiple toasts
    }
    if (formState.success) {
      toast.success("Xóa giá trị thuộc tính sản phẩm thành công!");
      router.push(`/dashboard/products/${productId}`);
    }
  }, [formState, toastDisplayed]);

  return (
    <>
      <div className="flex items-center justify-end mt-10">
        <Link
          href={{
            pathname: `/dashboard/product-values/add`,
            query: { productId },
          }}
        >
          <button className="p-2 flex items-center justify-center mb-5 bg-purple-600 text-white rounded">
            <MdAdd />
            Thêm Thuộc Tính Mới
          </button>
        </Link>
      </div>
      <table className="w-full text-left text-gray-400">
        <thead className="bg-gray-700 text-gray-400 uppercase">
          <tr>
            <th className="px-4 py-2">#</th>
            <th className="px-4 py-2">Thuộc tính sản phẩm</th>
            <th className="px-4 py-2">Giá trị</th>
            <th className="px-4 py-2">Trạng thái</th>
            <th className="px-4 py-2">Hành động</th>
          </tr>
        </thead>
        <tbody>
          {productValues?.map((value: IProductValue, index) => (
            <tr
              key={value.productAttributeId}
              className="border-b border-gray-700"
            >
              <td className="px-4 py-2">{index + 1}</td>
              <td className="px-4 py-2">{value.productAttribute.name}</td>
              <td className="px-4 py-2">{value.value}</td>
              <td className="px-4 py-2">
                <TagField
                  cssClass={value.isActive ? "bg-lime-900" : "bg-red-700"}
                  context={value.isActive ? "Hoạt động" : "Ngưng hoạt động"}
                />
              </td>
              <td className="px-4 py-2">
                <div className="flex gap-2">
                  <Link
                    href={{
                      pathname: `/dashboard/product-values/${value.productAttributeId}`,
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
                      name="productAttributeId"
                      value={value.productAttributeId}
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

export default ProductAttributeValueForm;
