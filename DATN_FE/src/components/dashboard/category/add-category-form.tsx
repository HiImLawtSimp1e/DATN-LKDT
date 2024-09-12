"use client";

import { addCategory } from "@/action/categoryAction";
import InputField from "@/components/ui/input";
import { useCustomActionState } from "@/lib/custom/customHook";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import slugify from "slugify";

const AddCategoryForm = () => {
  const router = useRouter();

  const initialState: FormState = { errors: [] };
  const [formState, formAction] = useCustomActionState<FormState>(
    addCategory,
    initialState
  );

  const [formData, setFormData] = useState({
    title: "",
    slug: "",
  });

  const [toastDisplayed, setToastDisplayed] = useState(false);

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const formData = new FormData(event.currentTarget);
    formAction(formData);
    setToastDisplayed(false); // Reset toastDisplayed when submitting
  };

  const handleChange = (
    e:
      | React.ChangeEvent<HTMLInputElement>
      | React.ChangeEvent<HTMLSelectElement>
  ) => {
    const { name, value } = e.target;
    setFormData((prevFormData) => ({
      ...prevFormData,
      [name]: value,
    }));
    if (name === "title") {
      setFormData((prevFormData) => ({
        ...prevFormData,
        slug: slugify(value, { lower: true }),
      }));
    }
  };

  useEffect(() => {
    if (formState.errors.length > 0 && !toastDisplayed) {
      toast.error("Tạo danh mục thất bại");
      setToastDisplayed(true); // Set toastDisplayed to true to prevent multiple toasts
    }
    if (formState.success) {
      toast.success("Tạo danh mục thành công!");
      router.push("/dashboard/category");
    }
  }, [formState, toastDisplayed]);

  return (
    <form onSubmit={handleSubmit} className="px-4 w-full">
      <InputField
        label="Tiêu đề"
        id="title"
        name="title"
        value={formData.title}
        onChange={handleChange}
        required
      />
      <InputField
        label="Slug"
        id="slug"
        name="slug"
        value={formData.slug}
        onChange={handleChange}
        readonly
      />
      {formState.errors.length > 0 && (
        <ul>
          {formState.errors.map((error, index) => (
            <li className="text-red-400" key={index}>
              {error}
            </li>
          ))}
        </ul>
      )}
      <button
        type="submit"
        className="float-right mt-4 text-white bg-blue-700 hover:bg-blue-800 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center"
      >
        Tạo mới
      </button>
    </form>
  );
};

export default AddCategoryForm;
