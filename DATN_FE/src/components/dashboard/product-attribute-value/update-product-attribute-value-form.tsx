"use client";

import { updateAttributeValue } from "@/action/productAttributeValueAction";
import InputField from "@/components/ui/input";
import SelectField from "@/components/ui/select";
import { useCustomActionState } from "@/lib/custom/customHook";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";

interface IProps {
  productValue: IProductValue;
}

const UpdateProductAttributeValueForm = ({ productValue }: IProps) => {
  const router = useRouter();

  const initialState: FormState = { errors: [] };
  const [formState, formAction] = useCustomActionState<FormState>(
    updateAttributeValue,
    initialState
  );

  const [formData, setFormData] = useState<IProductValue>(productValue);

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
      toast.error("Cập nhật giá trị thuộc tính sản phẩm thất bại");
      setToastDisplayed(true); // Đặt toastDisplayed là true để tránh hiển thị nhiều toast
    }
    if (formState.success) {
      toast.success("Cập nhật giá trị thuộc tính sản phẩm thành công!");
      router.push(`/dashboard/products/${productValue.productId}`);
    }
  }, [formState, toastDisplayed]);

  return (
    <form onSubmit={handleSubmit} className="px-4 w-full">
      <input type="hidden" name="productId" value={productValue.productId} />
      <input
        type="hidden"
        name="productAttributeId"
        value={productValue.productAttributeId}
      />
      <div className="mb-5">
        <label className="block mb-2 text-sm font-medium text-white">
          Thuộc tính
        </label>
        <input
          value={productValue.productAttribute.name}
          className="text-sm rounded-lg w-full p-2.5 bg-gray-600 placeholder-gray-400 text-white"
          readOnly
        />
      </div>
      <InputField
        label="Giá trị"
        id="value"
        name="value"
        value={formData.value}
        onChange={handleChange}
        required
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
        Cập Nhật
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

export default UpdateProductAttributeValueForm;
