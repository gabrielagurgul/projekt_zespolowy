//
//  BudgetFetcher.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 10/12/2021.
//

import Foundation

typealias ID = Int
typealias PLN = Double
fileprivate let session: URLSession = URLSession.shared


func getArrayOfBudgetCategories() async throws -> [BudgetType] {
	let urlRequest = try prepareUrlRequest(API.GET.budgetTypeDescription, method: .GET)
	let (data,_) = try await session.data(for: urlRequest)
	guard let arrayOfBudgetsCategories = try? JSONDecoder().decode([BudgetType].self, from: data) else {
		throw ApiError.invalidDataDecoding
	}
	return arrayOfBudgetsCategories
}

func getArrayOfBudgetsOf(type: ID) async throws -> [Budget] {
	let urlRequest = try prepareUrlRequest(API.GET.budgetDescription, method: .GET)
	let (data,_) = try await session.data(for: urlRequest)
	guard let arrayOfBudgets = try? JSONDecoder().decode([Budget].self, from: data) else {
		throw ApiError.invalidDataDecoding
	}
	return arrayOfBudgets
}

func getBudgetBy(_ id: ID) async throws -> Budget {
	let urlRequest = try prepareUrlRequest(API.GET.listOfBudgets, method: .GET)
	let (data,_) = try await session.data(for: urlRequest)
	
	return Budget.budgetMock
}

func addUserBudget(ammount: PLN) async throws -> Budget {
	var urlRequest = try prepareUrlRequest(API.POST.addBudget, method: .POST)
	guard let data = try? JSONEncoder().encode(Budget.budgetMock.data) else {
		throw ApiError.invalidDataEncoding
	}
	urlRequest.httpBody = data
	let (serverData,_) = try await session.data(for: urlRequest)
	guard let budgetAPIData = try? JSONDecoder().decode(Budget.BudgetAPI.self, from: serverData) else {
		throw ApiError.invalidDataDecoding
	}
	print(budgetAPIData)
	return Budget.budgetMock
}

func addUserSalary(ammount: PLN) async throws -> PLN {
	let urlRequest = try prepareUrlRequest(API.POST.addSalary, method: .POST)
	let (data,_) = try await session.data(for: urlRequest)
	guard let ammount = try? JSONDecoder().decode(PLN.self, from: data) else {
		throw ApiError.invalidDataDecoding
	}
	return ammount
}

func getUserExpensives() async throws -> [PLN] {
	[-10.0, -5.1, -3.2]
}

func getPredictionforType(_ id: ID, budget: Budget) async throws -> Double {
	return 0.8
}

enum REST: String {
	case GET = "GET"
	case PUT = "PUT"
	case DELETE = "DELETE"
	case POST = "POST"
}

enum ApiError: Error, LocalizedError {
	case invalidDataDecoding
	case invalidURL
	case invalidDataEncoding
	var errorDescription: String? {
		switch self {
		case .invalidDataDecoding:
			return "Nie udało się przekonwertować danych otrzymanych od serwera"
		case .invalidURL:
			return "Nie udało się utworzyć adresu url"
		case .invalidDataEncoding:
			return "Nie udało się utworzyć danych do przesłania na serwer"
		}
	}
}

private func prepareUrlRequest(_ apiURL: String, method: REST) throws -> URLRequest {
	guard let url = URL(string: apiURL) else {
		throw ApiError.invalidURL
	}
	var urlRequest = URLRequest(url: url)
	urlRequest.setValue("application/json", forHTTPHeaderField: "Content-Type")
	urlRequest.httpMethod = method.rawValue
	return urlRequest
}
