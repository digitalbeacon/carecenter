// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System;
using System.Collections.Generic;
using DigitalBeacon.Model;
using DigitalBeacon.CareCenter.Model;

namespace DigitalBeacon.CareCenter.Business
{
	public interface IClientService
	{
		#region Clients

		/// <summary>
		/// Gets the client.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		ClientEntity GetClient(long id);

		/// <summary>
		/// Gets the client by encrypted SSN.
		/// </summary>
		/// <param name="encryptedSsn">The encrypted SSN.</param>
		/// <returns></returns>
		ClientEntity GetClientByEncryptedSsn(string encryptedSsn);

		/// <summary>
		/// Get clients.
		/// </summary>
		/// <param name="searchInfo">The search info.</param>
		/// <returns></returns>
		IList<ClientEntity> GetClients(ClientSearchInfo searchInfo);

		/// <summary>
		/// Get client count.
		/// </summary>
		/// <param name="searchInfo">The search info.</param>
		/// <returns></returns>
		long GetClientCount(ClientSearchInfo searchInfo);

		/// <summary>
		/// Saves the client.
		/// </summary>
		/// <param name="client">The client.</param>
		/// <returns></returns>
		ClientEntity SaveClient(ClientEntity client);

		/// <summary>
		/// Deletes the client.
		/// </summary>
		/// <param name="clientId">The client id.</param>
		void DeleteClient(long clientId);

		/// <summary>
		/// Deletes the client photo.
		/// </summary>
		/// <param name="clientId">The client id.</param>
		void DeleteClientPhoto(long clientId);

		/// <summary>
		/// Updates the client location.
		/// </summary>
		/// <param name="clientId">The client id.</param>
		void UpdateClientLocation(long clientId);

		/// <summary>
		/// Updates the client household count.
		/// </summary>
		/// <param name="clientId">The client id.</param>
		void UpdateClientHouseholdCount(long clientId);

		/// <summary>
		/// Sets dormant clients to inactive status.
		/// </summary>
		/// <returns></returns>
		long SetDormantClientsToInactive();

		#endregion

		#region Queries

		/// <summary>
		/// Gets the unique visit years for the given client.
		/// </summary>
		/// <param name="clientId">The client id.</param>
		/// <returns></returns>
		IList<int> GetVisitYears(long clientId);

		///// <summary>
		///// Gets the clients that have visits for the given date range.
		///// </summary>
		///// <param name="minDate">The min date.</param>
		///// <param name="maxDate">The max date.</param>
		///// <returns></returns>
		//IList<ClientEntity> GetClientsForDateRange(DateTime minDate, DateTime maxDate);

		#endregion
	}
}
