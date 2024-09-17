"use client";

import { useEffect, useState } from "react";
import Chart from "@/components/dashboard/charts";
import Transactions from "@/components/dashboard/transactions";
import { getAuthPublic } from "@/service/auth-service/auth-service";
import AdminLoading from "@/components/dashboard/loading";
import AdminError from "@/components/dashboard/error";

const DashboardPage = () => {
  const [orders, setOrders] = useState<IOrder[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchOrders = async () => {
      try {
        const token = getAuthPublic();
        const res = await fetch("http://localhost:5000/api/Order/admin", {
          method: "GET",
          headers: {
            Authorization: `Bearer ${token}`, // Thêm header Authorization
          },
          cache: "no-store",
        });

        if (!res.ok) {
          throw new Error("Failed to fetch orders");
        }

        const responseData: ApiResponse<PagingParams<IOrder[]>> =
          await res.json();
        const { data } = responseData;
        const { result } = data;
        setOrders(result);
      } catch (err) {
        setError((err as Error).message);
      } finally {
        setLoading(false);
      }
    };

    fetchOrders();
  }, []);

  if (loading) {
    return <AdminLoading />;
  }
  if (error) {
    return <AdminError content={error} />;
  }
  return (
    <div className="flex flex-col">
      <Transactions orders={orders} />
      <Chart />
    </div>
  );
};

export default DashboardPage;
