"use client";

import { updateVariant } from "@/action/variantAction";
import InputField from "@/components/ui/input";
import SelectField from "@/components/ui/select";
import { useCustomActionState } from "@/lib/custom/customHook";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";

interface IProps {
  variant: IProductVariant;
}

const UpdateVariantForm = ({ variant }: IProps) => {
  const router = useRouter();

  const initialState: FormState = { errors: [] };
  const [formState, formAction] = useCustomActionState<FormState>(
    updateVariant,
    initialState
  );

  const [formData, setFormData] = useState<IProductVariant>(variant);

  const [toastDisplayed, setToastDisplayed] = useState(false);

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const formData = new FormData(event.currentTarget);
    formAction(formData);
    setToastDisplayed(false); // Đặt lại toastDisplayed khi đang submit
  };

  const handleChange = (
    e:
      | React.ChangeEvent<HTMLInputElement>
      | React.ChangeEvent<HTMLSelectElement>
  ) => {
    const { name, value } = e.target;

    // Kiểm tra nếu là InputField
    if (e.target instanceof HTMLInputElement) {
      setFormData((prevFormData) => ({
        ...prevFormData,
        [name]: value,
      }));
    }

    // Kiểm tra nếu là SelectField
    if (e.target instanceof HTMLSelectElement) {
      setFormData((prevFormData) => ({
        ...prevFormData,
        [name]: value,
      }));
    }
  };

  useEffect(() => {
    if (formState.errors.length > 0 && !toastDisplayed) {
      toast.error("Cập nhật biến thể sản phẩm thất bại");
      setToastDisplayed(true); // Đặt toastDisplayed là true để tránh hiển thị nhiều toast
    }
    if (formState.success) {
      toast.success("Cập nhật biến thể sản phẩm thành công!");
      router.push(`/dashboard/products/${variant.productId}`);
    }
  }, [formState, toastDisplayed]);

  return (
    <form onSubmit={handleSubmit} className="px-4 w-full">
      <input type="hidden" name="productId" value={variant.productId} />
      <input type="hidden" name="productTypeId" value={variant.productTypeId} />
      <InputField
        type="number"
        label="Giá bán"
        id="price"
        name="price"
        value={formData.price.toString()}
        onChange={handleChange}
        min-value={0}
        required
      />
      <InputField
        type="number"
        label="Giá gốc"
        id="originalPrice"
        name="originalPrice"
        value={formData.originalPrice.toString()}
        onChange={handleChange}
        min-value={0}
      />
      <InputField
        type="number"
        label="Số lượng"
        id="quantity"
        name="quantity"
        value={formData.quantity.toString()}
        onChange={handleChange}
        min-value={0}
        required
      />
      <label className="block mb-2 text-sm font-medium text-white">
        Loại sản phẩm
      </label>
      <input
        value={variant.productType.name}
        className="text-sm rounded-lg w-full p-2.5 bg-gray-600 placeholder-gray-400 text-white"
        readOnly
      />
      <SelectField
        label="Trạng thái"
        id="isActive"
        name="isActive"
        value={formData.isActive.toString()}
        onChange={handleChange}
        options={[
          { label: "Hoạt động", value: "true" },
          { label: "Ngưng hoạt động", value: "false" },
        ]}
      />
      <button
        type="submit"
        className="float-right mt-4 text-white bg-blue-700 hover:bg-blue-800 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center"
      >
        Cập Nhật Biến Thể
      </button>
      {formState.errors.length > 0 && (
        <ul>
          {formState.errors.map((error, index) => (
            <li className="text-red-400" key={index}>
              {error}
            </li>
          ))}
        </ul>
      )}
    </form>
  );
};

export default UpdateVariantForm;
