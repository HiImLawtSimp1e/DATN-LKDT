"use client";

import { FormEvent } from "react";
import { MdSearch } from "react-icons/md";

interface SearchFieldProps {
  placeholder: string;
  name?: string;
  onSubmit?: (event: FormEvent<HTMLFormElement>) => void; // Thêm callback
}

const Search: React.FC<SearchFieldProps> = ({
  placeholder,
  name,
  onSubmit,
}) => {
  return (
    <form onSubmit={onSubmit}>
      {/* Sử dụng onSearch callback */}
      <div className="flex items-center gap-2 bg-gray-700 p-2 rounded-lg w-max">
        <input
          type="text"
          name={name || ""}
          placeholder={placeholder}
          className="bg-transparent border-none text-white outline-none"
        />
        <button type="submit">
          <MdSearch />
        </button>
      </div>
    </form>
  );
};

export default Search;
